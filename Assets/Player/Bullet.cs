using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D m_refRigid;
    private Vector2 m_vDir = Vector2.zero;


    [SerializeField] private float m_fAliveTime = 5.0f;
    [SerializeField] LayerMask m_tHitLayer;

    private float m_fCurTime = 0.0f;

    [SerializeField] private float m_fSpeed = 3.0f;
    public void Init(Vector2 _vDir)
    {
        m_fCurTime = m_fAliveTime;
        m_vDir = _vDir;
    }

    private void Update()
    {
        m_fCurTime -= Time.deltaTime;
        if(m_fCurTime <= 0.0f)
        {
            //Pool
            Destroy(gameObject);
        }
    }
    private void Awake()
    {
        m_refRigid = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Vector2 vPos = m_refRigid.position;
        vPos.x += (m_vDir.x * m_fSpeed * Time.fixedDeltaTime);
        vPos.y += (m_vDir.y * m_fSpeed * Time.fixedDeltaTime);

        m_refRigid.MovePosition(vPos);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((m_tHitLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            var dam = other.GetComponent<ITakeDamageable>();
            dam.TakeDamage(transform.position);
            Destroy(gameObject);
        }
    }
}
