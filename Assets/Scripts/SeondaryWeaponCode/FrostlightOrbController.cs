using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostlightOrbController : MonoBehaviour
{
    public List<GameObject> frostlightOrbs; // List untuk menyimpan prefab Frostlight Orb
    public Transform player; // Referensi ke posisi pemain
    public float orbitRadius = 2f; // Jarak orbit dari pemain
    public float orbitSpeed = 50f; // Kecepatan rotasi dalam derajat per detik

    private float[] angles; // Array untuk menyimpan sudut masing-masing orb

    void Start()
    {
        // Inisialisasi sudut untuk setiap orb
        angles = new float[frostlightOrbs.Count];
        for (int i = 0; i < frostlightOrbs.Count; i++)
        {
            angles[i] = i * (360f / frostlightOrbs.Count); // Sebar sudut secara merata
        }
    }

    void Update()
    {
        for (int i = 0; i < frostlightOrbs.Count; i++)
        {
            if (frostlightOrbs[i] != null)
            {
                // Perbarui sudut rotasi
                angles[i] += orbitSpeed * Time.deltaTime;
                if (angles[i] >= 360f) angles[i] -= 360f;

                // Hitung posisi baru berdasarkan sudut dan radius
                Vector3 offset = new Vector3(
                    Mathf.Cos(angles[i] * Mathf.Deg2Rad) * orbitRadius,
                    Mathf.Sin(angles[i] * Mathf.Deg2Rad) * orbitRadius,
                    0f
                );

                // Set posisi orb relatif terhadap pemain
                frostlightOrbs[i].transform.position = player.position + offset;
            }
        }
    }
}
