using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObject/Player Data")]
public class PlayerData : ScriptableObject
{
    public float HP;
    public float speed;
    public float attackDamage;
    public float pickRange;
    public float armorPierce;
}
