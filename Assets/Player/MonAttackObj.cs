using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class MonAttackObj : MonoBehaviour
{
    [SerializeField] private LayerMask m_tAttackLayer;
    [SerializeField] private int m_iAttack;

    [SerializeField] private float m_fAliveTime = 0.1f;
    private float m_fCurTime = 0.0f;

    public void Update()
    {
        m_fCurTime += Time.deltaTime;
        if (m_fCurTime >= m_fAliveTime)
            Destroy(gameObject);
    }
    public void Init(int iDamage)
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
}
