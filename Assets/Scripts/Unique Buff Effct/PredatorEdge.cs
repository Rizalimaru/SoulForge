using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredatorEdge : MonoBehaviour
{
    public PlayerData playerData;
    public float bonusPercent = 20f; // 20% bonus damage
    public float hpThreshold = 0.3f; // 30% HP

    // Fungsi ini dipanggil sebelum damage diberikan ke musuh
    public float GetModifiedDamage(EnemyScript enemy)
    {
        if (enemy == null) return playerData.attackDamage;

        float hpRatio = enemy.HP / enemy.enemyData.HP;
        if (hpRatio < hpThreshold)
        {
            return playerData.attackDamage * (1f + bonusPercent / 100f);
        }
        else
        {
            return playerData.attackDamage;
        }
    }
}
