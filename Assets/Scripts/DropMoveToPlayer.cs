using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMoveToPlayer : MonoBehaviour
{   
    public PlayerData playerData; // Reference to the PlayerData scriptable object
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
            transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime * 10f);
            yield return null;
        }
        if(this.CompareTag("coin"))
        {
            playerData.coin += 1; // Increase the player's coin count
        }
        else if(this.CompareTag("exp"))
        {
            playerData.currnetXP += 10; // Increase the player's XP
        }
        else if(this.CompareTag("score"))
        {
            playerData.score += 1; // Increase the player's score
        }
        // Optionally, you can destroy the object after reaching the player
        Destroy(gameObject);
}
}
