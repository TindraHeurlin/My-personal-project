using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float bounceForce = 12f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.useGravity = true;

        // Initial jump to start movement
        Bounce();
    }

    void Update()
    {
        // Left–right control
        float horizontal = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector3(horizontal * moveSpeed, rb.linearVelocity.y, 0f);

        // Game-over check
        if (transform.position.y < -10f)
        {
            Debug.Log("Game Over!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Bounce only on platforms and only if landing from above
        if (collision.gameObject.CompareTag("Platform") && rb.linearVelocity.y <= 0f)
        {
            Bounce();
        }
    }

    void Bounce()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, bounceForce, 0f);
    }
}
