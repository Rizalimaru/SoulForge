using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObject/Player Data")]
public class PlayerData : ScriptableObject
{   
    [Header("Player Stats")]
    public float HP;
    public float speed;
    public float attackDamage;
    public float pickRadius;
    public float armorPierce;
    public float currnetXP;
    public float maxXP;
    public float level;

    [Header("Player Score and Currency")]
    public int score;
    public int coin;

}
