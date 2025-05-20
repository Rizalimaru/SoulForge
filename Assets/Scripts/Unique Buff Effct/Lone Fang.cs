using System.Collections;
using UnityEngine;

public class LoneFang : MonoBehaviour
{
    public PlayerData playerData;
    public float bonusPercent = 25f; // 25% attack speed bonus
    public float safeRadius = 5f;    // Radius tanpa musuh = bonus aktif
    public float dangerRadius = 3f;  // Radius musuh sangat dekat = bonus hilang

    private float originalAttackSpeed;
    private bool bonusActive = false;

    void Start()
    {
        originalAttackSpeed = playerData.attackSpeed;
    }

    void Update()
    {
        EnemyScript[] enemies = FindObjectsOfType<EnemyScript>();
        float nearestDist = float.MaxValue;

        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < nearestDist)
                nearestDist = dist;
        }

        if (nearestDist > safeRadius)
        {
            // Tidak ada musuh dekat, aktifkan bonus jika belum aktif
            if (!bonusActive)
            {
                playerData.attackSpeed = originalAttackSpeed - (playerData.baseAttackSpeed * (bonusPercent / 100f));
                if (playerData.attackSpeed < 0.05f) playerData.attackSpeed = 0.05f;
                bonusActive = true;
            }
        }
        else if (nearestDist < dangerRadius)
        {
            // Musuh sangat dekat, kembalikan attack speed ke semula jika bonus aktif
            if (bonusActive)
            {
                playerData.attackSpeed = originalAttackSpeed;
                bonusActive = false;
            }
        }
        // Jika musuh di antara safeRadius dan dangerRadius, biarkan status bonus tetap
    }

    public void ApplyBuff(BuffEffect effect)
    {
        switch (effect.statType)
        {
            case BuffStatType.AttackSpeed:
                playerData.attackSpeed -= playerData.baseAttackSpeed * (effect.value / 100f);
                if (playerData.attackSpeed < 0.05f) playerData.attackSpeed = 0.05f;
                break;
            // Tambahkan case lain untuk buff yang berbeda jika diperlukan
        }
    }
}
