using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormcallerBowController : MonoBehaviour
{
    [Header("Lightning Settings")]
    public GameObject lightningPrefab;      // Prefab sambaran petir
    public float initialDelay = 1f;         // Delay sebelum pertama kali mulai
    public float interval = 2f;             // Interval antar pengecekan
    public float DestroyDelay = 0.5f;         // Waktu sebelum menghancurkan sambaran petir
    [Range(0f, 1f)]
    public float chanceToStrike = 0.1f;     // Peluang untuk menyambar tiap musuh (0 - 1)

    private List<Transform> enemiesInRange = new List<Transform>();

    private void Start()
    {
        StartCoroutine(StrikeCoroutine());
    }

    private IEnumerator StrikeCoroutine()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            for (int i = enemiesInRange.Count - 1; i >= 0; i--)
            {
                Transform enemy = enemiesInRange[i];
                if (enemy == null)
                {
                    enemiesInRange.RemoveAt(i);
                    continue;
                }

                if (Random.value < chanceToStrike)
                {
                    GameObject lightning = Instantiate(lightningPrefab, enemy.position, Quaternion.identity);
                    Destroy(lightning, DestroyDelay);
                }
            }

            yield return new WaitForSeconds(interval);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Add(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemiesInRange.Remove(other.transform);
        }
    }
}
