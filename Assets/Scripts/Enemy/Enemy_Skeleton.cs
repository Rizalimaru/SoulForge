using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class Enemy_Skeleton : MonoBehaviour
{
    private Transform target;
    public Enemy_Data skeletonData;
    private Rigidbody2D rb;
    private float armor, speedReduction;
    public float HP;
    public float knockbackForce = 5f; // Kekuatan knockback
    public float knockbackDuration = 0.2f; // Durasi knockback
    private bool isKnockedBack = false;

    [Header("Drop Settings")]
    public GameObject coinPrefab; // Prefab untuk koin
    public GameObject expPrefab;  // Prefab untuk exp
    public int maxDrop = 10;      // Total maksimal item yang bisa dijatuhkan
    [Range(0f, 1f)]
    public float coinDropChance = 0.3f; // Peluang drop koin (30%)

    void Start()
    {
        HP = skeletonData.HP;
        armor = skeletonData.armor;
        speedReduction = skeletonData.speedReduction;
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isKnockedBack)
        {
            // Gerakan normal jika tidak sedang terkena knockback
            transform.position = Vector2.MoveTowards(transform.position, target.position, skeletonData.speed * Time.deltaTime);

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
        if (rb != null)
        {
            isKnockedBack = true;
            rb.velocity = Vector2.zero; // Reset kecepatan sebelumnya
            rb.AddForce(direction.normalized * knockbackForce, ForceMode2D.Impulse);
            StartCoroutine(ResetKnockback());
        }
    }

    private IEnumerator ResetKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
        rb.velocity = Vector2.zero; // Hentikan gerakan setelah knockback selesai
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
}
