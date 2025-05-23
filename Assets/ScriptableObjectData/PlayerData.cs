using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Data", menuName = "ScriptableObject/Player Data")]
public class PlayerData : ScriptableObject
{   
    [Header("Current Stats")]
    public float currentHP;
    public float currnetXP;
    public float currentSpeed;
    [Header("Player Stats")]
    public float maxHP;
    public float speed;
    public float attackDamage;
    public float attackSpeed;
    public float pickRadius;
    public float armorPierce;
    public float maxXP;
    public float level;
    public float knockbackForce;
    public float regen;
    
    [Header("Base Stats")]
    public float baseHP;
    public float baseSpeed;
    public float baseAttackSpeed;
    public float baseAttackDamage;
    public float basePickRadius;
    public float baseArmorPierce;
    public float baseknockbackForce;
    public float baseRegen;
    public float baseMaxXP;
    
    [Header("Coin And Score In Stage")]
    public int coinInStage;
    public int scoreInStage;

    [Header("Player Score and Currency")]
    public int score;
    public int coin;

    [Header("Shop Upgrade Progress")]
    public int[] shopUpgradeLevels = new int[6]; // 6 stat, simpan level upgrade tiap stat

}
