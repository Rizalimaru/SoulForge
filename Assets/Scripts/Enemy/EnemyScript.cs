using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private Transform target;
    public Enemy_Data enemyData;
    private float speedReduction;
    private SpriteRenderer spriteRenderer;
    public PlayerData playerData; // Referensi ke PlayerData
    //public float HP;
    public float knockbackForce = 5f; // Kekuatan knockback
    public float knockbackDuration = 0.2f; // Durasi knockback
    private bool isKnockedBack = false;
    private Rigidbody2D rb; // Tambahkan Rigidbody2D untuk knockback

    [Header("Interval Damage")]
    public float damageInterval = 1f; // Interval waktu untuk mengurangi HP player
    private float damageTimer = 0f; // Timer untuk menghitung waktu

    [Header("Drop Settings")]
    public GameObject coinPrefab; // Prefab untuk koin
    public GameObject expPrefab;  // Prefab untuk exp
    public int maxDrop = 10;      // Total maksimal item yang bisa dijatuhkan
    [Range(0f, 1f)]
    public float coinDropChance = 0.3f; // Peluang drop koin (30%)

    public float speedInstance; // Speed yang dipakai di Update

    private bool isBlinking = false;
    private float _hp;
    public float HP
    {
        get => _hp;
        set
        {
            if (value < _hp) // Hanya jika HP berkurang
            {
                if (!isBlinking)
                    StartCoroutine(getDamageAnimation());
            }
            _hp = value;
        }
    }


    void Start()
    {
        // Jangan set HP dan speed di sini, gunakan InitStats saat spawn!
        target = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!isKnockedBack)
        {
            // Gerakan normal jika tidak sedang terkena knockback
            Vector2 direction = (target.position - transform.position).normalized;
            rb.velocity = direction * enemyData.speed;

            // Atur orientasi musuh berdasarkan posisi pemain
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
            playerData.scoreInStage += Random.Range(1, 10); // Tambahkan score saat musuh mati
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
            rb.velocity = direction * knockbackForce; // Gunakan Rigidbody2D untuk knockback
            timer += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero; // Hentikan gerakan setelah knockback selesai
        isKnockedBack = false; // Kembali ke status normal
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
        if (collision.gameObject.CompareTag("Player"))
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                // Cek kemungkinan miss dari FracturedNerves
                FracturedNerves fracturedNerves = collision.gameObject.GetComponent<FracturedNerves>();
                if (fracturedNerves != null && fracturedNerves.IsMiss())
                {
                    Debug.Log("Enemy attack missed due to Fractured Nerves!");
                    damageTimer = 0f; // Tetap reset timer walau miss
                    return;
                }

                playerData.currentHP -= enemyData.damage; // Kurangi HP player
                damageTimer = 0f; // Reset timer
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            damageTimer = 0f; // Reset timer saat keluar dari collision
        }
    }

    public void InitStats(float statMultiplier)
    {
        HP = enemyData.HP * statMultiplier;
        speedReduction = enemyData.speedReduction;
        // Jika ingin speed bertambah, gunakan field baru:
        speedInstance = enemyData.speed * statMultiplier;
    }

    public IEnumerator getDamageAnimation()
    {
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();

        float blinkDuration = 0.5f;
        float blinkInterval = 0.1f;
        float timer = 0f;
        bool transparent = false;

        Color originalColor = spriteRenderer.color;
        Color transparentColor = originalColor;
        transparentColor.a = 0.3f; // Atur transparansi

        while (timer < blinkDuration)
        {
            spriteRenderer.color = transparent ? transparentColor : originalColor;
            transparent = !transparent;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }
        spriteRenderer.color = originalColor; // Kembalikan warna semula
    }
}
