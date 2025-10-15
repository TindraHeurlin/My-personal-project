using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject powerUpPrefab;  // drag your PowerUp prefab here in the Inspector
    public float spawnRate = 10f;     // how often to spawn
    public float xRange = 4f;         // how far left/right from center
    public float yOffset = 15f;       // how far above player to spawn

    private Transform player;

    void Start()
    {
        // Find the player in the scene
        player = GameObject.FindWithTag("Player").transform;

        // Start spawning repeatedly
        InvokeRepeating(nameof(SpawnPowerUp), 2f, spawnRate);
    }

    void Update()
    {

    }
    
    void SpawnPowerUp()
    {
        // If player is gone, stop spawning
        if (player == null) return;

        // Pick a random X position near the middle
        float randomX = Random.Range(-xRange, xRange);

        // Spawn slightly above the player’s current height
        Vector3 spawnPos = new Vector3(randomX, player.position.y + yOffset, -5f);

        // Create the PowerUp
        Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);

        // Print message for debugging
        //Debug.Log("Spawned PowerUp at: " + spawnPos); 
    }

}
