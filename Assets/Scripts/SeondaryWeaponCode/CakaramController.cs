using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakaramController : MonoBehaviour
{
    public Transform player;
    public float radius;
    public float rotationSpeed;
    private float currentAngle = 0f;
    public SecondaryWeaponData cakramData;

    void Update()
    {
        if (player != null)
        {
            currentAngle += rotationSpeed * Time.deltaTime;
            if (currentAngle >= 360f) currentAngle -= 360f;

            // Hitung posisi baru di sumbu X-Y
            float radian = currentAngle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * radius;

            transform.position = player.position + offset;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyScript enemy = collision.GetComponent<EnemyScript>();
            if (enemy != null && cakramData != null)
            {
                // Hitung damage berdasarkan level saat ini
                int currentLevel = Mathf.Min(cakramData.levels.Count - 1, 0); // Default ke level 0 jika tidak ada level
                float damage = cakramData.baseDamage * cakramData.levels[currentLevel].damageMultiplier;

                // Kurangi HP musuh
                enemy.HP -= damage;

                Debug.Log($"Enemy HP: {enemy.HP}, Damage Dealt: {damage}");
            }
        }
    }

}
