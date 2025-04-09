using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMoveToPlayer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickRadius"))
        {
            // Start moving the object towards the player's position
            Transform playerTransform = GameObject.FindWithTag("Player").transform;
            StartCoroutine(MoveToPlayer(playerTransform));
        }
    }

    IEnumerator MoveToPlayer(Transform player)
    {
        // Move the object towards the player's position over time
        while (Vector2.Distance(transform.position, player.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * 5f);
            yield return null;
        }

        // Optionally, you can destroy the object after reaching the player
        Destroy(gameObject);
}
}
