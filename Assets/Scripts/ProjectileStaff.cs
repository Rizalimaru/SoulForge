using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileStaff : MonoBehaviour
{
    public PlayerData playerData;
    public Vector2 moveDirection; // Tambahkan ini
    public float speed = 10f;     // Atur kecepatan sesuai kebutuhan


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Get the Enemy component from the collided object
            EnemyScript enemy = other.GetComponent<EnemyScript>();

            // Check if the enemy is not null
            if (enemy != null)
            {
                // Call the TakeDamage method on the enemy
                enemy.HP -= playerData.attackDamage;

                Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                enemy.ApplyKnockback(knockbackDirection);
            }
        }
    }

    void Update()
    {
        // Bergerak maju ke arah moveDirection
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
    }
}
