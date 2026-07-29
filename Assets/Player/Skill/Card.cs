using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static SKillControler;
using static UnityEditor.LightingExplorerTableColumn;

public class Card : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private SkillData Data;
    [SerializeField] private Image m_refImage;

    private CardCreator m_refOwner;

    public void OnPointerClick(PointerEventData eventData)
    {
        //북에 던지기, 스킬 매니저에 던지기
        m_refOwner.SelectCard(Data);
    }


    public void Init(SkillData SkillData , CardCreator _refOwner)
    {
        Data = SkillData;
        m_refImage.sprite = SkillData.Image;
      
        m_refOwner = _refOwner;
    }
}
