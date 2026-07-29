using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStay : AttackObj
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if ((m_tAttackLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            var dam = other.GetComponent<ITakeDamageable>();
            dam.TakeDamage(transform.position, m_iAttack);
            Debug.Log(gameObject.name);
        }
    }
}
