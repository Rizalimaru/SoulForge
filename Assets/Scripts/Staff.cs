using System.Collections;
using UnityEngine;

public class Staff : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint; // Titik spawn projectile
    public PlayerData playerData;

    private float shootTimer = 0f;

    void Update()
    {
        shootTimer -= Time.deltaTime;
        Debug.Log("Shoot Timer: " + shootTimer); // Debug log untuk melihat nilai shootTimer
        if (shootTimer <= 0f)
        {
            ShootProjectile();
            shootTimer = playerData.attackSpeed; // Reset timer sesuai attack speed
        }
    }

    void ShootProjectile()
    {
        // Hitung arah mouse
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mouseWorldPos - firePoint.position).normalized;

        // Jika prefab menghadap ke Y (atas), tambahkan -90 derajat pada rotasi
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle - 20, Vector3.forward);

        // Spawn projectile dengan rotasi yang sesuai
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, rot);
        ProjectileStaff projScript = proj.GetComponent<ProjectileStaff>();
        if (projScript != null)
        {
            projScript.playerData = playerData;
            projScript.moveDirection = direction; // Set arah gerak
        }
        Destroy(proj, 5f); // Hapus projectile setelah 5 detik
    }
}
