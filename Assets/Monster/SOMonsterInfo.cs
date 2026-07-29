using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOMonsterInfo", menuName = "Monster/Data")]

public class SOMonsterInfo : ScriptableObject
{
    public int MaxHP = 100;
    public int Speed = 2;
    public int Damage = 5;
    public int Exp = 10;
}
