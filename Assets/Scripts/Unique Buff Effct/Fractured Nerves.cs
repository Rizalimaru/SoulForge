using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FracturedNerves : MonoBehaviour
{
    public PlayerData playerData;
    public float missChance = 0.3f; // 30% kemungkinan miss
    public float hpThreshold = 0.5f; // 50% HP

    public bool IsMiss()
    {
        if (playerData == null) return false;
        float hpRatio = playerData.currentHP / playerData.maxHP;
        if (hpRatio < hpThreshold)
        {
            return Random.value < missChance;
        }
        return false;
    }
}
