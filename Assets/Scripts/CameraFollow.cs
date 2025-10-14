using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;       // Reference to the player
    public float offsetY = 2f;     // Vertical offset so player stays lower in the frame

    private float highestY;        // Track the highest point the camera has reached

    void Start()
    {
        // If no player is assigned, find one tagged "Player"
        if (player == null)
        {
            player = GameObject.FindWithTag("Player").transform;
        }

        // Record the camera's starting Y position
        highestY = transform.position.y;
    }

    void LateUpdate()
    {
        // Only move camera upward when player climbs higher
        if (player.position.y > highestY)
        {
            highestY = player.position.y;
        }

        // Follow the player upward (keep X and Z the same)
        transform.position = new Vector3(
            transform.position.x,
            highestY + offsetY,
            transform.position.z
        );
    }
}
