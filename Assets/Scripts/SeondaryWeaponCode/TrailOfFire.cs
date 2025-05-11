using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailOfFire : MonoBehaviour
{
    public SecondaryWeaponData secondaryWeaponData;
    Animator animator;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyScript enemy = collision.GetComponent<EnemyScript>();
            if (enemy != null && secondaryWeaponData != null)
            {
                // Hitung damage berdasarkan level saat ini
                int currentLevel = Mathf.Min(secondaryWeaponData.levels.Count - 1, 0); // Default ke level 0 jika tidak ada level
                float damage = secondaryWeaponData.baseDamage * secondaryWeaponData.levels[currentLevel].damageMultiplier;

                // Kurangi HP musuh
                enemy.HP -= damage;

                Debug.Log($"Enemy HP: {enemy.HP}, Damage Dealt: {damage}");
            }
        }
    }
}
