using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static SKillControler;


public interface ITakeDamageable
{
    public void TakeDamage(Vector2 vHitPos, int _iDamage);


}

public class Player : MonoBehaviour, ITakeDamageable
{
    [SerializeField] private Slider m_refHPSlider;
    [SerializeField] private Slider m_refExpSlider;

    public event Action OnLevelUP;
    public UnityEvent OnDead;

    private Vector2 m_vInput;

    private Rigidbody2D m_refRigid;

    [SerializeField] private float m_fSpeed = 2.0f;
    [SerializeField] private float m_fFireTime = 0.2f;
    [SerializeField] private float m_fOffset = 2.0f;
    [SerializeField] private Bullet m_refBullet;
    [SerializeField] private AnimTable m_refTable;
    private float m_fLastFireTime = 0.0f;

    private SpriteRenderer m_refRener;

    public static Player m_player;

    public static Player MainPlayer => m_player;

    [SerializeField] private List<SOItem> m_listItem;

    eEntityState m_eState = eEntityState.Idle;
    Color m_tOriginColor;
    [SerializeField] private Color m_tChangeColor;

    [SerializeField] private SKillControler m_refSkill;

    private Coroutine m_COHit = null;

    public static Vector2 MOUSE_POS;

    private int m_iDamage = 30;
    private int m_iHP = 100;
    private int m_iMax = 100;
    private int m_iEXP = 100;

    [SerializeField] private CardCreator m_refCardCreator;

    private int m_iLevel = 0;
    [SerializeField] private TextMeshProUGUI m_refLevelText;

    private bool dead = false;
    private void Awake()
    {
        m_iHP = m_iMax;
        m_player = this;
        m_fLastFireTime = Time.time;
        m_refRener = GetComponent<SpriteRenderer>();
        m_refRigid = GetComponent<Rigidbody2D>();

        m_tOriginColor = m_refRener.color;
    }

    private void Start()
    {
        Monster.OnDead += AddEXP;
    }
    private void Update()
    {
        if (dead) return;
        m_vInput.x = Input.GetAxis("Horizontal");
        m_vInput.y = Input.GetAxis("Vertical");
        if (m_vInput.magnitude >= 0.1f)
            m_refTable.SetBool(eEntityState.Run, true);
        else
            m_refTable.SetBool(eEntityState.Run, false);

     
        if(m_vInput.x > 0)
            m_refRener.flipX = false;
        else
            m_refRener.flipX = true;



        if (Input.GetKey(KeyCode.Space))
        {
            Fire();


            Vector2 vPos = transform.position;
            Vector2 vDiff = MOUSE_POS - vPos;
            Vector2 vNor = vDiff.normalized;

            vPos += vNor * m_fOffset * 3;

            m_refSkill.ShotSkill(vPos);
        }

        if (Input.GetKeyDown(KeyCode.O))
            AppHP();
        if (Input.GetKeyDown(KeyCode.P))
            AppSpeed();
    }

    private void FixedUpdate()
    {
        if (dead) return;

        Vector2 vPos = m_refRigid.position;
        vPos.x += (m_vInput.x * m_fSpeed * Time.fixedDeltaTime);
        vPos.y += (m_vInput.y * m_fSpeed * Time.fixedDeltaTime);

        m_refRigid.MovePosition(vPos);
    }

    private void Fire()
    {
        if ((Time.time - m_fLastFireTime) < m_fFireTime)
            return;

        m_fLastFireTime = Time.time;
        m_refTable.SetTrigger(eEntityState.Attack);

        Spawn();
    }

    private void Spawn()
    {
        MOUSE_POS = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 vPos = transform.position;
        Vector2 vDiff = MOUSE_POS - vPos;
        Vector2 vNor = vDiff.normalized;

        vPos += vNor * m_fOffset;

        Bullet refBullet = GameObject.Instantiate(m_refBullet, vPos, Quaternion.identity);
        refBullet.Init(vNor, m_iDamage);
    }

    
    private IEnumerator Hit()
    {
        m_refRener.color = m_tChangeColor;
        yield return new WaitForSeconds(0.8f);
        m_refRener.color = m_tOriginColor;
    }
    public void TakeDamage(Vector2 vHitPos, int _iDamage)
    {
        m_iHP -= _iDamage;
        m_refHPSlider.value = ((float)m_iHP / (float)m_iMax);
        if(m_iHP <= 0.0f)
        {
            m_refTable.SetTrigger(eEntityState.Dead);
        }
        else
        {
            m_refTable.SetTrigger(eEntityState.Hit);

            if (m_COHit != null)
                StopCoroutine(m_COHit);
            m_COHit = StartCoroutine(Hit());
        }
       
    }

    //CallBack
    public void AddEXP(int _value)
    {
        m_refExpSlider.value += ( (float)_value/ (float)m_iEXP);
        if(m_refExpSlider.value >= 1.0f)
        {
            m_refExpSlider.value = 0;
            //스킬
            ++m_iLevel;
            m_refCardCreator.StartCard();

            m_refLevelText.text = m_iLevel.ToString();
        }
    }

    public void AddSkill(eSkillKey _eSkill)
    {
        m_refSkill.UnLockSkill(_eSkill);
    }


    public void Dead()
    {
        OnDead?.Invoke();
        gameObject.SetActive(false);
    }


    public void AppHP()
    {
        for(int i = 0; i<m_listItem.Count;++i)
        {
            var list = m_listItem[i].ListValue;

            for(int j = 0; j < list.Count; ++j)
            {
                if (list[j].Type == eStatType.HP)
                {
                    ApplyHP((int)list[j].Value);
                    return;
                }
            }
        }
    }

    public void AppSpeed()
    {
        for (int i = 0; i < m_listItem.Count; ++i)
        {
            var list = m_listItem[i].ListValue;

            for (int j = 0; j < list.Count; ++j)
            {
                if (list[j].Type == eStatType.Speed)
                {
                    ApplySpeed((int)list[j].Value);
                    return;
                }
            }
        }
    }
    public void ApplyHP(int hp)
    {
        m_iHP += hp;
        m_refHPSlider.value = ((float)m_iHP / (float)m_iMax);
    }

    public void ApplySpeed(float speed)
    {
        m_fSpeed += speed;
    }    
}
