using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public Door linkedDoor; // The door this leads to
    private static bool isOnCooldown = false; // Shared cooldown to prevent re-entry
    private static Camera mainCamera; // Cache the main camera

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main; // Get the main camera
    }

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

            // Move the camera to the new room
            MoveCameraToRoom(linkedDoor.transform.position);

            // Wait for 1 second before allowing teleportation again
            yield return new WaitForSeconds(1f);

            isOnCooldown = false; // Cooldown over
        }
        else
        {
            Debug.LogWarning("No linked door assigned!");
        }
    }

    private void MoveCameraToRoom(Vector2 newPosition)
    {
        // Find the nearest CameraPoint
        GameObject[] cameraPoints = GameObject.FindGameObjectsWithTag("CameraPoint");
        GameObject closestPoint = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject point in cameraPoints)
        {
            float distance = Vector2.Distance(newPosition, point.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPoint = point;
            }
        }

        // Move the camera to the closest CameraPoint
        if (closestPoint != null)
        {
            mainCamera.transform.position = new Vector3(
                closestPoint.transform.position.x,
                closestPoint.transform.position.y,
                mainCamera.transform.position.z
            );
        }
        else
        {
            Debug.LogWarning("No CameraPoint found!");
        }
    }
}
