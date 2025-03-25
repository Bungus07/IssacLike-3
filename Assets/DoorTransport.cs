using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public Door linkedDoor; // The door this door is linked to
    private static bool isOnCooldown = false; // Shared cooldown for all doors

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOnCooldown)
        {
            StartCoroutine(TeleportPlayer(other.gameObject));
        }
    }

    private IEnumerator TeleportPlayer(GameObject player)
    {
        if (linkedDoor != null)
        {
            isOnCooldown = true; // Start cooldown

            // Move the player slightly outside the linked door to prevent instant re-triggering
            Vector2 exitPosition = (Vector2)linkedDoor.transform.position + (Vector2)linkedDoor.transform.up * 1.5f;
            player.transform.position = exitPosition;

            // Wait for 1 second before allowing teleportation again
            yield return new WaitForSeconds(1f);

            isOnCooldown = false; // Cooldown over
        }
        else
        {
            Debug.LogWarning("No linked door assigned!");
        }
    }
}
