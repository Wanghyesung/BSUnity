using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "SOITem", menuName = "Game/Item")]
public class SOItem : ScriptableObject
{
    [SerializeField] private List<tStatValue> m_listValue = new List<tStatValue>();
    public IReadOnlyList<tStatValue> ListValue => m_listValue;
}


public enum eStatType
{
    HP,
    Speed,
    End,
}

[Serializable]
public struct tStatValue
{
    public eStatType Type;
    public float Value;
}