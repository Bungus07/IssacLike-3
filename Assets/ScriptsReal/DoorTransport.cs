using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour
{
    public Door linkedDoor; // The door this leads to
    private static bool isOnCooldown = false; // Shared cooldown to prevent re-entry
    private static Camera mainCamera; // Cache the main camera
    [SerializeField] private bool DoorIsLeftOrDown;
    public bool IsLocked;
    private GameObject Player;
    
    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main; // Get the main camera
        Player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isOnCooldown)
        {
            StartCoroutine(TeleportPlayer(other.gameObject));
        }
    }
    private void UnlockDoor()
    {
        Player.gameObject.GetComponent<PlayerController>().KeyCount--;
        // This is where you change the sprite
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.transform.GetChild(0).gameObject.SetActive(false); // if change sprite DELETE THIS
        StartCoroutine(TeleportPlayer(Player));
    }
    private void Start()
    {
        if (IsLocked)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("DoorHasCollided");
        if (other.gameObject.tag == "Player")
        {
            if (IsLocked)
            {
                Debug.Log("IsLocked");
                if (other.gameObject.GetComponent<PlayerController>().KeyCount > 0)
                {
                    UnlockDoor();
                }
            }
        }
    }
    private IEnumerator TeleportPlayer(GameObject player)
    {
        if (linkedDoor != null)
        {
            isOnCooldown = true; // Start cooldown
            if (!DoorIsLeftOrDown)
            {
                // Move the player slightly outside the linked door to prevent instant re-triggering
                Vector2 exitPosition = (Vector2)linkedDoor.transform.position + (Vector2)linkedDoor.transform.right * 1.5f;
                player.transform.position = exitPosition;
            }
            else
            {
                Vector2 exitPosition = (Vector2)linkedDoor.transform.position +- (Vector2)linkedDoor.transform.right * 1.5f;
                player.transform.position = exitPosition;
            }

                // Move the camera to the new room
                MoveCameraToRoom(linkedDoor.transform.position);

            // Wait for 1 second before allowing teleportation again
            Player.GetComponent<PlayerController>().IsInvincible = true;
            yield return new WaitForSeconds(0.5f);

            isOnCooldown = false; // Cooldown over
            yield return new WaitForSeconds(Player.GetComponent<PlayerController>().InvunrableTime);
            Player.GetComponent<PlayerController>().IsInvincible = false;
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