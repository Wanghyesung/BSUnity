using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, ITakeDamageable
{
    [SerializeField] private float HP = 100.0f;

    [SerializeField] private Rigidbody2D m_refRigid;
    [SerializeField] private float m_fSpeed = 2.0f;

    [SerializeField] private float m_fAttackLen = 1.0f;
    private SpriteRenderer m_refRener;

    private float m_fCurLen = 0.0f;
    private Vector2 m_vDir;

    Color m_tOriginColor;
    [SerializeField] private Color m_tChangeColor;
    [SerializeField] private AnimTable m_refTable;

    [SerializeField] private LayerMask m_tAttackLayer;

    [SerializeField] private AttackObj AttakObj;
    [SerializeField] private float m_fOffset = 1.0f;

    private bool m_bHit = false;
    private bool m_bAttack = false;

    private Coroutine m_COHit = null;

    private void Awake()
    {
        m_refRigid = GetComponent<Rigidbody2D>();
        m_refRener = GetComponent<SpriteRenderer>();
        m_tOriginColor = m_refRener.color;

    }


    private void Update()
    {
        if (m_bHit == true || m_bAttack == true)
            return;

        var Target = Player.MainPlayer;

        Vector3 vTargetPos =  Target.transform.position;
        Vector2 vTarget2D = new Vector2(vTargetPos.x, vTargetPos.y);

        Vector2 diff = vTarget2D - m_refRigid.position;
        m_fCurLen = diff.magnitude;


        m_vDir = diff.normalized;
        Vector2 vPos = m_refRigid.position;

        if(m_vDir.magnitude >= 0.1f)
            m_refTable.SetBool(eEntityState.Run, true);
        else
            m_refTable.SetBool(eEntityState.Run, false);


        m_refRigid.MovePosition(vPos + (m_vDir * m_fSpeed * Time.fixedDeltaTime));


        if (diff.x > 0)
            m_refRener.flipX = false;
        else
            m_refRener.flipX = true;

        CheckAttack();
    }

    private void CheckAttack()
    {
        if(m_fCurLen <= m_fAttackLen)
        {
            m_bAttack = true;
            m_refTable.SetTrigger(eEntityState.Attack);
        }
    }

    public void ChangeIdle()
    {
        m_bHit = false;
        m_bAttack = false;
    }

    public void Attack()
    {
        Vector2 vPos = m_refRigid.position;
        vPos += m_vDir * m_fOffset;

        var attack = GameObject.Instantiate(AttakObj, vPos, Quaternion.identity);
        attack.Init(10);
    }
    

    public void TakeDamage(Vector2 vHitPos)
    {
        HP -= 30.0f;
        m_bHit = true;
        if (HP <= 0.0f)
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

    public void Des()
    {
        Destroy(gameObject);
    }

    private IEnumerator Hit()
    {
        m_refRener.color = m_tChangeColor;
        yield return new WaitForSeconds(0.2f);
        m_refRener.color = m_tOriginColor;
    }
}
