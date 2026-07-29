using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [Serializable]
    public class SkillInfo
    {
        public float CoolTime = 0.2f;
        public AttackObj m_refSKill;
        public int iDamage;
        [NonSerialized] public float fLastFireTime = 0.0f;
    }

    [SerializeField] private List<SkillInfo> m_listSKill;

    private void Awake()
    {
        for (int i = 0; i < m_listSKill.Count; ++i)
        {
            m_listSKill[i].fLastFireTime = Time.time;
        }
    }

    public void ShotSkill()
    {
        float fCurTime = Time.time;
        for(int i = 0; i<m_listSKill.Count; ++i)
        {
            if ((fCurTime - m_listSKill[i].fLastFireTime) < m_listSKill[i].CoolTime)
                return;

            m_listSKill[i].fLastFireTime = Time.time;
            Spawn(Player.MOUSE_POS, m_listSKill[i].m_refSKill, m_listSKill[i].iDamage);
        }
    }


    private void Spawn(Vector2 _vPos, AttackObj _refFreFab, int _iDamage)
    {
        AttackObj refAttack = GameObject.Instantiate(_refFreFab, _vPos, _refFreFab.transform.rotation);
        refAttack.Init(_iDamage);
    }

}
