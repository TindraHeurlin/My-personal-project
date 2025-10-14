using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Transform gameCamera; // reference to your main camera
    public float moveSpeed = 5f;
    public float bounceForce = 12f;
    public float initialBounce = 24f; // fixed spelling

    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerRb.freezeRotation = true;
        playerRb.useGravity = true;

        // Delayed initial bounce to ensure physics has started
        Invoke(nameof(InitialBounce), 0.1f);
    }

    void InitialBounce()
    {
        // Apply a stronger first jump
        playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, initialBounce, 0f);
    }

    void Update()
    {
        // Left–right control
        float horizontal = Input.GetAxis("Horizontal");
        playerRb.linearVelocity = new Vector3(horizontal * moveSpeed, playerRb.linearVelocity.y, 0f);

        // Game-over check
        if (transform.position.y < gameCamera.position.y - 20f)
        {
            FindFirstObjectByType<GameManager>().GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Bounce only on platforms and only if landing from above
        if (collision.gameObject.CompareTag("Platform") && playerRb.linearVelocity.y <= 0f)
        {
            Bounce();
        }
    }

    void Bounce()
    {
        playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, bounceForce, 0f);
    }
}
