using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject startCanvas;
    public GameObject gameOverCanvas;
    private PlayerController player; //Player reference
    private bool isGameActive = false;
    private bool isGameOver = false;

    //Rank System
    public int powerUpCount = 0;
    public string currentRank = "Pawn";
    public TextMeshProUGUI rankText; 
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI rankResultText;

    //Level Speed System
    public int currentLevel = 1;
    public int score = 0;
    public float levelHeight = 100f;
    private float nextLevelY = 100f;

    //Difficulty Settings
    public float scrollSpeedIncrease = 0.2f; // how much faster the world scrolls each level

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    startCanvas.SetActive(true); // Show start screen
    Time.timeScale = 0f; // Pause the game until the player starts
    gameOverCanvas.SetActive(false); // Hide Game Over screen
        isGameActive = false;
        isGameOver = false;
    player = FindFirstObjectByType<PlayerController>();
    nextLevelY = levelHeight;
    
    // Rank Text always displayed at Start
    if (rankText != null)
    rankText.text = "Rank: " + currentRank;

    }

    // Update is called once per frame
    void Update()
    {
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

        // Show starting speed when the game begins
        RepeatSection section = FindFirstObjectByType<RepeatSection>();
        if (section != null && speedText != null)
        {
            speedText.text = "Chess Speed: " + section.scrollSpeed.ToString("F1") + "x";
        }

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

        if (rankResultText != null)
        rankResultText.text = 
        "Through every move and misstep,\n" +
        "you’ve earned the title of " + currentRank;

    }

    // Reloads the current scene so the player can play again
    public void RestartGame()
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
        float newSpeed = 0f;

        foreach (RepeatSection section in sections)
        {
            section.scrollSpeed += scrollSpeedIncrease;
            newSpeed = section.scrollSpeed; // save the current value
        }

        // Always display the current scroll speed
        if (speedText != null)
        {
            speedText.text = "Chess Speed: " + newSpeed.ToString("F1") + "x";

        }

        // Debug.Log("Level " + currentLevel + " reached. New scroll speed: " + newSpeed);
    }


    public void AddScore(int amount)
    {
        score += amount;
        powerUpCount += amount; // count powerups for rank system
        UpdateRank();           // check if we level up in rank
        Debug.Log("Score: " + score);
    }
    
    public void UpdateRank()
    {
        string previousRank = currentRank;

        if (powerUpCount < 1)
            currentRank = "Pawn";
        else if (powerUpCount < 2)
            currentRank = "Knight";
        else if (powerUpCount < 3)
            currentRank = "Rook";
        else if (powerUpCount < 4)
            currentRank = "Bishop";
        else
            currentRank = "Queen";

        if (rankText != null)
            rankText.text = "Rank: " + currentRank;
    }
}


