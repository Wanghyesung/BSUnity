using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKillControler : MonoBehaviour
{
    [SerializeField]
    public enum eSkillType
    {
        Far,
        Near,
    }

    public enum eSkillKey
    {
        Skill1,
        Skill2,
        Skill3,
        Skill4,
        Skill5,
    }

    [Serializable]
    public class SkillInfo
    {
        public bool Lock = true;
        public float CoolTime = 0.2f;
        public AttackObj m_refSKill;
        public int iDamage;
        public eSkillType eType;
        public eSkillKey eKey;
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

    public void ShotSkill(Vector2 _vNearPos)
    {
        float fCurTime = Time.time;
        for(int i = 0; i<m_listSKill.Count; ++i)
        {
            if (m_listSKill[i].Lock == true)
                continue;

            if ((fCurTime - m_listSKill[i].fLastFireTime) < m_listSKill[i].CoolTime)
                continue;

            m_listSKill[i].fLastFireTime = Time.time;
            if (m_listSKill[i].eType == eSkillType.Far)
                Spawn(Player.MOUSE_POS, m_listSKill[i].m_refSKill, m_listSKill[i].iDamage);
            else
                Spawn(_vNearPos, m_listSKill[i].m_refSKill, m_listSKill[i].iDamage);

        }
    }


    private void Spawn(Vector2 _vPos, AttackObj _refFreFab, int _iDamage)
    {
        AttackObj refAttack = GameObject.Instantiate(_refFreFab, _vPos, _refFreFab.transform.rotation);
        refAttack.Init(_iDamage);
    }

    public void UnLockSkill(eSkillKey _eSKillKey)
    {
        for(int i = 0; i<m_listSKill.Count; ++i)
        {
            if (m_listSKill[i].eKey == _eSKillKey)
                m_listSKill[i].Lock = false;
        }
    }

}
