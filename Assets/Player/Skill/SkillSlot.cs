using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillData skillData;


    [SerializeField] private Image m_refImage;


    public void Bind(SkillData _skillData)
    {
        m_refImage.sprite = _skillData.Image;
        skillData = _skillData;
    }
}
