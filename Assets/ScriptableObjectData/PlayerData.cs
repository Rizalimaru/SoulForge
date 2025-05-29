using System.Collections;
using System.Collections.Generic;
using System.IO;
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

    [Header("Tutorial Data")]
    public bool isTutorialCompleted = false; // Menyimpan status apakah tutorial sudah selesai
    public bool isintroEndlessComplated = false; // Menyimpan status apakah intro endless sudah selesai


    [Header("Player Score and Currency")]
    public int score;
    public int coin;

    [Header("Shop Upgrade Progress")]
    public int[] shopUpgradeLevels = new int[6]; // 6 stat, simpan level upgrade tiap stat


    private static string SavePath => Path.Combine(Application.persistentDataPath, "playerdata.json");

    [System.Serializable]
    public class SaveStruct
    {
        public int coin;
        public int score;
        public int[] shopUpgradeLevels;
        // Tambahkan field lain yang ingin disimpan
    }

    public void Save()
    {
        SaveStruct data = new SaveStruct
        {
            coin = this.coin,
            score = this.score,
            shopUpgradeLevels = (int[])this.shopUpgradeLevels.Clone()
        };
        File.WriteAllText(SavePath, JsonUtility.ToJson(data));
    }

    public void Load()
    {
        if (File.Exists(SavePath))
        {
            SaveStruct data = JsonUtility.FromJson<SaveStruct>(File.ReadAllText(SavePath));
            this.coin = data.coin;
            this.score = data.score;
            this.shopUpgradeLevels = data.shopUpgradeLevels ?? new int[6];
        }
    }
}
