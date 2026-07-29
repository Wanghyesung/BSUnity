using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
    [SerializeField] List<SkillSlot> m_listSkillSlot = new();

    [SerializeField] RectTransform m_refContent;

    [SerializeField] SkillSlot m_refSlot;

    
    public void AddData(SkillData _refData)
    {
        if (m_listSkillSlot.Find(slot => slot.skillData == _refData) != null)
            return;

        var t = Instantiate(m_refSlot, m_refContent);
        m_listSkillSlot.Add(t);
        t.Bind(_refData);
    }

    public void DeleteData(SkillData _refData)
    {
        int idx = m_listSkillSlot.FindIndex(x => x.skillData == _refData);
        if (idx == -1)
            return;

        var slot = m_listSkillSlot[idx];
        Destroy(slot);
        m_listSkillSlot[idx] = null;
    }

    public void OnSKill()
    {
        m_refContent.gameObject.SetActive(!m_refContent.gameObject.activeSelf);
            
    }
}
