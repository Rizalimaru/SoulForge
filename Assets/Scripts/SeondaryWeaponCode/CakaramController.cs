using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CakaramController : MonoBehaviour
{
    public Transform player;
    public float radius;
    public float rotationSpeed;

    private float currentAngle = 0f;

    void Update()
    {
        if(player != null)
        {
            currentAngle += rotationSpeed * Time.deltaTime;
            if (currentAngle >= 360f) currentAngle -= 360f;

            // Hitung posisi baru di sumbu X-Y
            float radian = currentAngle * Mathf.Deg2Rad;
            Vector3 offset = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * radius;

            transform.position = player.position + offset;
        }
    }

}
