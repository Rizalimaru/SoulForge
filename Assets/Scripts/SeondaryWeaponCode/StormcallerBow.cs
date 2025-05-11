using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormcallerBow : MonoBehaviour
{
    public SecondaryWeaponData stormcallerData;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyScript enemy = collision.GetComponent<EnemyScript>();
            if (enemy != null && stormcallerData != null)
            {
                // Hitung damage berdasarkan level saat ini
                int currentLevel = Mathf.Min(stormcallerData.levels.Count - 1, 0); // Default ke level 0 jika tidak ada level
                float damage = stormcallerData.baseDamage * stormcallerData.levels[currentLevel].damageMultiplier;

                // Kurangi HP musuh
                enemy.HP -= damage;

                Debug.Log($"Enemy HP: {enemy.HP}, Damage Dealt: {damage}");
            }
        }
    }
}
