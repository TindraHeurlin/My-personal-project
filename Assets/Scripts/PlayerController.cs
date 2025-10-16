using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Transform gameCamera; // reference to your main camera
    public float moveSpeed = 5f;
    public float bounceForce = 12f;
    public float initialBounce = 17;//old 24f; // fixed spelling
    public float fastFall = 2.5f; //arrowkey down fall
    public ParticleSystem jumpParticle;
    private bool isBoosted = false; // check if powerup is active
    private bool isGameOver = false;


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

        // Down arrow to fall faster 
        if (Input.GetKey(KeyCode.DownArrow) && playerRb.linearVelocity.y <= 0f)
        {
            playerRb.linearVelocity += Vector3.down * fastFall;
        }

        // Game-over check
        if (!isGameOver && transform.position.y < gameCamera.position.y - 20f)
        {
            isGameOver = true; // mark that the game has ended

            // Play sound once
            FindFirstObjectByType<SoundEffects>().PlayGameOver();

            // Trigger Game Over logic (UI + freeze)
            FindFirstObjectByType<GameManager>().GameOver();
         }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBoosted)
        {
            // Skip bouncing while ghost mode is active
            return;
        }

        // Bounce normally when landing on a platform
        if (collision.gameObject.CompareTag("Platform") && playerRb.linearVelocity.y <= 0f)
        {
            Bounce();
        }
    }


    void Bounce()
    {
        // If the player has a power-up
        if (isBoosted)
            {
                playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, bounceForce * 1.5f, 0f);
            }
        // Otherwise, use the normal bounce
        else
            {
            playerRb.linearVelocity = new Vector3(playerRb.linearVelocity.x, bounceForce, 0f);
            jumpParticle.Play();
            FindFirstObjectByType<SoundEffects>().PlayBounce();
                

            }
    }


    //PowerUp Trigger
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
    
            // Tell the GameManager to add score
            GameManager gameManager = FindFirstObjectByType<GameManager>();
            if (gameManager != null)
            {
                FindFirstObjectByType<SoundEffects>().PlayPowerUp();
                gameManager.AddScore(1);
            }

            // Print to the console for debugging
            Debug.Log("PowerUp collected! +1 point");

            // Destroy the PowerUp object so it's "collected"
            Destroy(other.gameObject);
        }
    }


  


}
