using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class AttackObj : MonoBehaviour
{
    [SerializeField] private LayerMask m_tAttackLayer;
    [SerializeField] private int m_iAttack;

    [SerializeField] private float m_fAliveTime = 0.1f;
    private float m_fCurTime = 0.0f;

    [SerializeField] private AttackObj m_refDeadSpawn = null;

    int flag = 0;
    public virtual void Update()
    {
        m_fCurTime += Time.deltaTime;
        if (m_fCurTime >= m_fAliveTime)
            Dead();
    }
    public virtual void Init(int iDamage)
    {
        m_iAttack = iDamage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((m_tAttackLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            var dam = other.GetComponent<ITakeDamageable>();
            dam.TakeDamage(transform.position);
        }
    }

    public void Dead()
    {
        if (flag > 0)
            return;

        ++flag;

        if(m_refDeadSpawn != null)
        {
            AttackObj refAttack = 
                GameObject.Instantiate(m_refDeadSpawn, transform.position, Quaternion.identity);

            refAttack.Init(m_iAttack);
        }
        Destroy(gameObject);
    }
}
