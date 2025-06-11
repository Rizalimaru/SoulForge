using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public static float xpMultiplier = 3f; // XP multiplier global
    public float enemyCountIncreaseRate = 0.1f; // Persentase peningkatan jumlah musuh per gelombang (10%)
    public float baseSpawnInterval = 2f; // Bisa diubah di Inspector

    private int currentWaveIndex = 0; // Indeks gelombang saat ini
    private bool isSpawning = false; // Apakah sedang melakukan spawn
    private int currentEnemyCount; // Jumlah musuh di gelombang saat ini

    public GameObject secondaryWeaponSelectionUI;
    public GameObject primaryWeaponSelectionUI; // Tambahkan ini di atas (drag di Inspector)
    public SecondaryWeaponSelectionUI secondaryWeaponSelectionUIManager; // Referensi ke UI pemilihan senjata sekunder
    public TextMeshProUGUI waveText;

    private Coroutine waveMessageCoroutine;



    void Start()
    {
        currentEnemyCount = initialEnemyCount; // Set jumlah musuh awal
        StartCoroutine(StartWaveSystem());
        waveText.enabled = false;
    }

    IEnumerator StartWaveSystem()
    {
        while (true) // Loop tanpa batas untuk gelombang
        {
            // Tampilkan pesan wave mulai
            yield return WaitForOtherUI(); // Tunggu UI lain benar-benar hilang sebelum tampilkan pesan

            ShowWaveText($"Wave {currentWaveIndex + 1} mulai!", 2f);

            Debug.Log("Starting Wave: " + (currentWaveIndex + 1));
            Wave currentWave = GenerateWave();

            yield return StartCoroutine(SpawnWave(currentWave));

            while (!IsWaveComplete())
            {
                yield return null;
            }

            Debug.Log("Wave " + (currentWaveIndex + 1) + " completed!");

            // Tampilkan pesan wave selesai setelah UI lain ditutup
            yield return WaitForOtherUI();
            ShowWaveText($"Wave {currentWaveIndex + 1} selesai!", 2f);

            // Hentikan waktu dan tampilkan UI untuk secondary weapon selection
            Time.timeScale = 0;
            secondaryWeaponSelectionUI.SetActive(true);

            // Tunggu hingga pemain menutup UI (misalnya, dengan tombol konfirmasi)
            while (secondaryWeaponSelectionUI.activeSelf)
            {
                yield return null;
            }

            // Lanjutkan waktu setelah UI ditutup
            Time.timeScale = 1;
            // Tambahkan jumlah musuh untuk gelombang berikutnya
            currentEnemyCount = Mathf.CeilToInt(currentEnemyCount * (1 + enemyCountIncreaseRate));
            // Contoh: jika awal 10, rate 0.1, maka wave 2 = 11, wave 3 = 12, dst.

            // Set XP multiplier berdasarkan wave (misal naik 10% per wave)
            xpMultiplier = 1f + (currentWaveIndex * 0.1f);
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
            enemies = enemies,
            enemyCount = currentEnemyCount,
            spawnInterval = Mathf.Max(0.5f, baseSpawnInterval - (currentWaveIndex * 0.1f))
        };

        return newWave;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        int spawnedEnemies = 0;
        isSpawning = true;

        // Faktor peningkatan stat per wave (misal: 10% per wave)
        float statMultiplier = 1f + (currentWaveIndex * 0.1f);

        while (spawnedEnemies < wave.enemyCount)
        {
            EnemySpawnData randomEnemy = wave.enemies[Random.Range(0, wave.enemies.Count)];
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            GameObject enemyObj = Instantiate(randomEnemy.enemyPrefab, randomSpawnPoint.position, Quaternion.identity);

            EnemyScript enemyScript = enemyObj.GetComponent<EnemyScript>();
            if (enemyScript != null)
            {
                enemyScript.InitStats(statMultiplier);
            }

            spawnedEnemies++;
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        isSpawning = false;
    }

    public bool IsWaveComplete()
    {
        // Periksa apakah semua musuh di gelombang saat ini sudah mati
        return isSpawning == false && GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    // Pastikan hanya satu pesan waveText yang tampil dalam satu waktu
    void ShowWaveText(string message, float duration)
    {
        if (waveMessageCoroutine != null)
            StopCoroutine(waveMessageCoroutine);
        waveMessageCoroutine = StartCoroutine(ShowWaveMessage(message, duration));
    }

    IEnumerator ShowWaveMessage(string message, float duration)
    {
        waveText.text = message;
        waveText.enabled = true;
        yield return new WaitForSecondsRealtime(duration);
        waveText.enabled = false;
    }

    // Tunggu hingga semua UI lain sudah tidak aktif sebelum menampilkan pesan wave
    IEnumerator WaitForOtherUI()
    {
        // Tunggu hingga secondaryWeaponSelectionUI dan primaryWeaponSelectionUI sudah tidak aktif
        while ((secondaryWeaponSelectionUI != null && secondaryWeaponSelectionUI.activeSelf)
            || (primaryWeaponSelectionUI != null && primaryWeaponSelectionUI.activeSelf))
        {
            yield return null;
        }
    }
}
