using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomAlly : MonoBehaviour
{
    public Transform player;
    public float orbitRadius = 2.5f;
    public float moveSpeed = 5f;
    public float attackCooldown = 1.5f;
    public float attackDamage = 10f;

    private Vector2 targetOffset;
    private Dictionary<EnemyScript, float> enemyCooldowns = new Dictionary<EnemyScript, float>();
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        PickNewTargetOffset();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null) return;

        // Gerak ke target offset random di sekitar player
        Vector2 targetPos = (Vector2)player.position + targetOffset;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        // Jika sudah dekat dengan target, pilih titik baru
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            PickNewTargetOffset();
        }

        // Update cooldowns
        var keys = new List<EnemyScript>(enemyCooldowns.Keys);
        foreach (var enemy in keys)
        {
            enemyCooldowns[enemy] -= Time.deltaTime;
            if (enemyCooldowns[enemy] <= 0f)
                enemyCooldowns.Remove(enemy);
        }

        // Flip sprite sesuai posisi musuh terdekat
        FlipToNearestEnemy();
    }

    void PickNewTargetOffset()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float radius = Random.Range(orbitRadius * 0.7f, orbitRadius * 1.3f);
        targetOffset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        EnemyScript enemy = other.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            if (!enemyCooldowns.ContainsKey(enemy))
            {
                enemy.HP -= attackDamage;
                enemyCooldowns[enemy] = attackCooldown;
            }
        }
    }

    void FlipToNearestEnemy()
    {
        if (spriteRenderer == null || player == null) return;

        EnemyScript[] enemies = FindObjectsOfType<EnemyScript>();
        EnemyScript nearest = null;
        float minDist = float.MaxValue;
        foreach (var enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = enemy;
            }
        }

        if (nearest != null)
        {
            // Bandingkan posisi musuh dengan player
            if (nearest.transform.position.x < player.position.x)
                spriteRenderer.flipX = true; // Musuh di kiri
            else
                spriteRenderer.flipX = false; // Musuh di kanan
        }
    }
}
