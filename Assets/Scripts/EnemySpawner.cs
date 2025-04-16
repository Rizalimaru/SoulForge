using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;
    public int pointCost;
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public float pointGainRate = 1f; // poin per detik
    public float spawnInterval = 1f; // waktu antar percobaan spawn
    public List<EnemySpawnData> enemies;

    private float currentPoints = 0f;
    private float spawnTimer = 0f;

    void Update()
    {
        // Tambah poin seiring waktu
        currentPoints += pointGainRate * Time.deltaTime;

        // Hitung waktu spawn
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            TrySpawnEnemy();
            spawnTimer = 0f;
        }
    }

    void TrySpawnEnemy()
    {
        // Acak daftar agar spawn musuh tidak selalu sama urutannya
        var shuffledEnemies = new List<EnemySpawnData>(enemies);
        Shuffle(shuffledEnemies);

        foreach (var enemyData in shuffledEnemies)
        {
            if (enemyData.pointCost <= currentPoints)
            {
                // Spawn musuh
                Instantiate(enemyData.enemyPrefab, transform.position, Quaternion.identity);
                currentPoints -= enemyData.pointCost;
                break; // satu musuh per percobaan
            }
        }
    }

    // Fungsi untuk mengacak daftar musuh (Fisher-Yates Shuffle)
    void Shuffle(List<EnemySpawnData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rnd = Random.Range(i, list.Count);
            var temp = list[i];
            list[i] = list[rnd];
            list[rnd] = temp;
        }
    }
}
