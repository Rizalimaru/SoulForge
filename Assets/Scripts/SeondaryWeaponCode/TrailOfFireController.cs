using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailOfFireController : MonoBehaviour
{
    public SecondaryWeaponData secondaryWeaponData;
    public GameObject fireTrailPrefab;
    public float spawnInterval = 0.5f; // Interval waktu antara setiap spawn

    void Start()
    {
        StartCoroutine(spawnFireTrail());  
    }

    IEnumerator spawnFireTrail()
    {
        while (true)
        {
            // Spawn fire trail prefab di posisi objek ini
            GameObject fireTrail = Instantiate(fireTrailPrefab, transform.position, Quaternion.identity);

            // Jalankan Coroutine untuk menghancurkan fire trail setelah waktu tertentu
            StartCoroutine(DestroyFireTrailAfterTime(fireTrail));

            // Tunggu sesuai interval sebelum spawn berikutnya
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    IEnumerator DestroyFireTrailAfterTime(GameObject fireTrail)
    {
        Animator fireTrailAnimator = fireTrail.GetComponent<Animator>();

        // Tunggu hingga waktu dari secondaryWeaponData.time habis
        yield return new WaitForSeconds(secondaryWeaponData.time);

        if (fireTrailAnimator != null)
        {
            // Mainkan animasi akhir sebelum menghancurkan fire trail
            fireTrailAnimator.SetTrigger("ApiSelesai");

            // Tunggu hingga animasi selesai
            yield return new WaitForSeconds(fireTrailAnimator.GetCurrentAnimatorStateInfo(0).length);
        }

        // Hancurkan fire trail
        Destroy(fireTrail);
    }



}
