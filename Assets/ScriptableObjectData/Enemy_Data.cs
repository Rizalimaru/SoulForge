using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_Data", menuName = "ScriptableObject/Enemy_Data")]
public class Enemy_Data : ScriptableObject
{
    public float speed;
    public float HP;
    public float damage;
    public float armor;
    public float speedReduction;
}
