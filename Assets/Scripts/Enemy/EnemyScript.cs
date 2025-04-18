using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Transform target;
    public Enemy_Data enemyData;
    private float speedReduction;
    public PlayerData playerData; // Referensi ke PlayerData
    public float HP;
    public float knockbackForce = 5f; // Kekuatan knockback
    public float knockbackDuration = 0.2f; // Durasi knockback
    private bool isKnockedBack = false;
    [Header("Interval Damage")]
    public float damageInterval = 1f; // Interval waktu untuk mengurangi HP player
    private float damageTimer = 0f; // Timer untuk menghitung waktu

    [Header("Drop Settings")]
    public GameObject coinPrefab; // Prefab untuk koin
    public GameObject expPrefab;  // Prefab untuk exp
    public int maxDrop = 10;      // Total maksimal item yang bisa dijatuhkan
    [Range(0f, 1f)]
    public float coinDropChance = 0.3f; // Peluang drop koin (30%)

    void Start()
    {
        HP = enemyData.HP;
        speedReduction = enemyData.speedReduction;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if(!isKnockedBack)
        {
            // Gerakan normal jika tidak sedang terkena knockback
            transform.position = Vector2.MoveTowards(transform.position, target.position, enemyData.speed * Time.deltaTime);

            if (target.transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (HP <= 0)
        {
            DropItems(); // Panggil fungsi drop item saat musuh mati
            Destroy(gameObject);
        }
    }

    public void ApplyKnockback(Vector2 direction)
    {
        if (!isKnockedBack)
        {
            isKnockedBack = true;
            StartCoroutine(PerformKnockback(direction.normalized));
        }
    }


    private IEnumerator PerformKnockback(Vector2 direction)
    {
        float timer = 0f;
        while (timer < knockbackDuration)
        {
            transform.Translate(direction * knockbackForce * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }

        isKnockedBack = false;
    }

        private void DropItems()
    {
        int totalDrops = Random.Range(1, maxDrop + 1); // Tentukan jumlah total item yang akan dijatuhkan

        for (int i = 0; i < totalDrops; i++)
        {
            float dropChance = Random.value; // Nilai acak antara 0 dan 1
            Vector2 randomOffset = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));
            Vector2 dropPosition = (Vector2)transform.position + randomOffset; // Posisi jatuh item

            if (dropChance < coinDropChance)
            {
                // Drop koin
                Instantiate(coinPrefab, dropPosition, Quaternion.identity);
            }
            else
            {
                // Drop exp
                Instantiate(expPrefab, dropPosition, Quaternion.identity);
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;
            if(damageTimer >= damageInterval)
            {
                playerData.currentHP -= enemyData.damage; // Kurangi HP player
                damageTimer = 0f; // Reset timer
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            damageTimer = 0f; // Reset timer saat keluar dari collision
        }
    }

}
