using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    public GameObject enemyPrefab;
    public int pointCost;
}

[System.Serializable]
public class Wave
{
    public string waveName; // Nama gelombang (opsional)
    public List<EnemySpawnData> enemies; // Daftar musuh yang akan muncul di gelombang ini
    public int enemyCount; // Jumlah total musuh di gelombang ini
    public float spawnInterval; // Waktu antar spawn musuh
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Wave Settings")]
    public List<EnemySpawnData> enemies; // Daftar semua jenis musuh
    public Transform[] spawnPoints; // Titik spawn musuh
    public float timeBetweenWaves = 5f; // Waktu jeda antar gelombang
    public int initialEnemyCount = 10; // Jumlah musuh awal di gelombang pertama
    public float enemyCountIncreaseRate = 0.1f; // Persentase peningkatan jumlah musuh per gelombang (10%)

    private int currentWaveIndex = 0; // Indeks gelombang saat ini
    private bool isSpawning = false; // Apakah sedang melakukan spawn
    private int currentEnemyCount; // Jumlah musuh di gelombang saat ini

    public GameObject secondaryWeaponSelectionUI;
    public SecondaryWeaponSelectionUI secondaryWeaponSelectionUIManager; // Referensi ke UI pemilihan senjata sekunder

    void Start()
    {
        currentEnemyCount = initialEnemyCount; // Set jumlah musuh awal
        StartCoroutine(StartWaveSystem());
    }

    IEnumerator StartWaveSystem()
    {
        while (true) // Loop tanpa batas untuk gelombang
        {
            Debug.Log("Starting Wave: " + (currentWaveIndex + 1));

            // Buat gelombang baru
            Wave currentWave = GenerateWave();

            // Mulai spawn musuh untuk gelombang saat ini
            yield return StartCoroutine(SpawnWave(currentWave));

            // Tunggu hingga semua musuh di gelombang ini dikalahkan
            while (!IsWaveComplete())
            {
                yield return null; // Tunggu satu frame
            }

            Debug.Log("Wave " + (currentWaveIndex + 1) + " completed!");

            // Hentikan waktu dan tampilkan UI untuk secondary weapon selection
            Time.timeScale = 0;
            secondaryWeaponSelectionUI.SetActive(true);
            secondaryWeaponSelectionUIManager.DisplayRandomWeapons(); // Tampilkan senjata secara acak

            // Tunggu hingga pemain menutup UI (misalnya, dengan tombol konfirmasi)
            while (secondaryWeaponSelectionUI.activeSelf)
            {
                yield return null; // Tunggu satu frame
            }

            // Lanjutkan waktu setelah UI ditutup
            Time.timeScale = 1;

            // Tingkatkan jumlah musuh untuk gelombang berikutnya
            currentEnemyCount = Mathf.CeilToInt(currentEnemyCount * (1 + enemyCountIncreaseRate));

            // Lanjutkan ke gelombang berikutnya
            currentWaveIndex++;

            Debug.Log("Next wave starting in " + timeBetweenWaves + " seconds...");
            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }

    Wave GenerateWave()
    {
        Wave newWave = new Wave
        {
            waveName = "Wave " + (currentWaveIndex + 1),
            enemies = enemies, // Gunakan daftar musuh yang ada
            enemyCount = currentEnemyCount, // Jumlah musuh berdasarkan perhitungan
            spawnInterval = Mathf.Max(0.5f, 2f - (currentWaveIndex * 0.1f)) // Kurangi interval spawn secara bertahap
        };

        return newWave;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        int spawnedEnemies = 0;
        isSpawning = true;

        while (spawnedEnemies < wave.enemyCount)
        {
            // Pilih musuh secara acak dari daftar musuh
            EnemySpawnData randomEnemy = wave.enemies[Random.Range(0, wave.enemies.Count)];

            // Pilih titik spawn secara acak
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Spawn musuh
            Instantiate(randomEnemy.enemyPrefab, randomSpawnPoint.position, Quaternion.identity);
            spawnedEnemies++;

            // Tunggu sebelum spawn musuh berikutnya
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        isSpawning = false;
    }

    public bool IsWaveComplete()
    {
        // Periksa apakah semua musuh di gelombang saat ini sudah mati
        return isSpawning == false && GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }
}
