using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormcallerBow : MonoBehaviour
{
    public SecondaryWeaponData stormcallerData;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyScript enemy  = collision.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                enemy.HP -= stormcallerData.damage; // Mengurangi HP musuh

                Debug.Log("Enemy HP: " + enemy.HP);
            }
        }
    }
}
