using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject gameOverCanvas;
    public TextMeshProUGUI levelText;
    private bool isGameActive = false;
    private bool isGameOver = false;

    [Header("Level System")]
    public int currentLevel = 1;
    public float levelHeight = 100f;
    private float nextLevelY = 100f;

    [Header("Difficulty Settings")]
    public float scrollSpeedIncrease = 0.2f; // how much faster the world scrolls each level


    // Reference to the player
    private PlayerController player;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    // Pause the game until the player starts
    Time.timeScale = 0f;

    // Show start screen
    startCanvas.SetActive(true);

    // Hide Game Over screen (in case it’s active from previous session)
    gameOverCanvas.SetActive(false);

    isGameActive = false;
    isGameOver = false;
    
    player = FindFirstObjectByType<PlayerController>();
        nextLevelY = levelHeight;
    
    levelText.text = "LEVEL " + currentLevel;



        
    }

    // Update is called once per frame
    void Update()
    {
        // Start the game when player presses Space
        if (!isGameActive && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }

        // If the game is over and the player presses R, restart the game
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }

        //Check level
        if (isGameActive && Camera.main != null && Camera.main.transform.position.y > nextLevelY)
        {
            currentLevel++;
            nextLevelY += levelHeight;
        }
    }
    public void StartGame()
    {
        isGameActive = true;
        Time.timeScale = 1f;

        startCanvas.SetActive(false);
        gameOverCanvas.SetActive(false);

        Debug.Log("Game Started!");
    }

    // This method is called when the player falls off the map
    public void GameOver()
    {
        // If the game is already over, do nothing
        if (isGameOver)
        {
            return;
        }

        // Set our flag to true
        isGameOver = true;

        // Stop time so the game freezes
        Time.timeScale = 0f;

        // Show Game Over UI
        gameOverCanvas.SetActive(true);

    }

    // Reloads the current scene so the player can play again
    void RestartGame()
    {
        // Resume normal time speed
        Time.timeScale = 1f;

        // Reload the active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NextLevel()
    {
    if (!isGameActive) return;

    currentLevel++;

    // Find every RepeatSection in the scene and increase its scroll speed
    RepeatSection[] sections = FindObjectsByType<RepeatSection>(FindObjectsSortMode.None);
    foreach (RepeatSection section in sections)
    {
        section.scrollSpeed += scrollSpeedIncrease;
    }

    levelText.text = "LEVEL " + currentLevel;
    }


}
