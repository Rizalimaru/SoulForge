using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailOfFire : MonoBehaviour
{
    public SecondaryWeaponData secondaryWeaponData;
    Animator animator;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyScript enemy  = collision.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.HP -= secondaryWeaponData.damage; // Mengurangi HP musuh

                Debug.Log("Enemy HP: " + enemy.HP);
            }
        }
    }
}
