using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Sword : MonoBehaviour
{   
    public Vector2 PointerPosition { get; set; }
    public Animator animator;
    private bool attackBlock;
    public Transform circleOrigin;
    public float circleRadius;
    public PlayerData playerData;
    
    void Update()
    {   
        Vector2 direction = (PointerPosition - (Vector2)transform.position).normalized;
        transform.right = direction;

        Vector2 scale  = transform.localScale;

        if(direction.x < 0)
        {
            scale.y = -1;
        }else if (direction.x > 0)
        {
            scale.y = 1;
        }

        transform.localScale = scale;
        attack();
    }

    public void attack()
    {   
        if(attackBlock) return;
        animator.SetTrigger("Attack");
        attackBlock = true;
        StartCoroutine(AttcakBlocked());
    }

    private IEnumerator AttcakBlocked()
    {
        yield return new WaitForSeconds(playerData.attackSpeed);
        attackBlock = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 position = circleOrigin == null? Vector3.zero : circleOrigin.position;
        Gizmos.DrawWireSphere(position, circleRadius);
    }

    public void DetectColliders()
    {
        PredatorEdge predatorEdge = GetComponent<PredatorEdge>(); // Ambil komponen jika ada

        foreach(Collider2D collider in Physics2D.OverlapCircleAll(circleOrigin.position, circleRadius))
        {
            Debug.Log(collider.name);
            EnemyScript enemy = collider.GetComponent<EnemyScript>();
            if (enemy != null)
            {
                float damage = playerData.attackDamage;
                if (predatorEdge != null)
                {
                    damage = predatorEdge.GetModifiedDamage(enemy);
                }

                // Kurangi HP skeleton
                enemy.HP -= damage;
                
                Vector2 knockbackDirection = (enemy.transform.position - transform.position).normalized;
                enemy.ApplyKnockback(knockbackDirection);

                Debug.Log($"Hit {collider.name}, remaining HP: {enemy.enemyData.HP}");
            }
        }
    }
}
