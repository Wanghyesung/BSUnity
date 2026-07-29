using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using static SKillControler;

[Serializable]
public class SkillData
{
    public eSkillKey Key;
    public Sprite Image;
}

public class CardCreator : MonoBehaviour
{
    [SerializeField] private List<SkillData> m_listSkill = new List<SkillData>();

    [SerializeField] private List<Card> m_listCard = new List<Card>();

    private HashSet<int> m_hash = new HashSet<int>();
    private List<int> m_listIdx = new List<int>();

   

    public void StartCard()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0.0f;

        int cunt = 0;
        m_hash.Clear();
        m_listIdx.Clear();
        while (cunt < 3)
        { 
            int idx = UnityEngine.Random.Range(0, m_listSkill.Count);
            if (m_hash.Contains(idx))
                continue;

            m_hash.Add(idx);
            m_listIdx.Add(idx);
            ++cunt;
           
        }



        for(int i = 0; i<m_listIdx.Count; ++i)
        {
            m_listCard[i].Init(m_listSkill[m_listIdx[i]], this);
        }
    }


    public void SelectCard(eSkillKey _eKey)
    {
        Player.MainPlayer.AddSkill(_eKey);

        Time.timeScale = 0.0f;
        gameObject.SetActive(false);
    }
}
