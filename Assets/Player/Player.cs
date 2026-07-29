using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ITakeDamageable
{
    public void TakeDamage(Vector2 vHitPos);


}

public class Player : MonoBehaviour, ITakeDamageable
{
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


    eEntityState m_eState = eEntityState.Idle;

    private void Awake()
    {
        m_player = this;
        m_fLastFireTime = Time.time;
        m_refRener = GetComponent<SpriteRenderer>();
        m_refRigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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



        if (Input.GetKeyDown(KeyCode.Space))
            Fire();

    }

    private void FixedUpdate()
    {
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
        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 vPos = transform.position;
        Vector2 vDiff = mouseWorldPos - vPos;
        Vector2 vNor = vDiff.normalized;

        vPos += vNor * m_fOffset;

        Bullet refBullet = GameObject.Instantiate(m_refBullet, vPos, Quaternion.identity);
        refBullet.Init(vNor);
    }

    public void TakeDamage(Vector2 vHitPos)
    {

    }

    private void Hit()
    {

    }

}
