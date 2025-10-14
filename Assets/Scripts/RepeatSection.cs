using UnityEngine;

public class RepeatSection : MonoBehaviour
{
    public float scrollSpeed = 2f;       // how fast the background scrolls
    public float sectionHeight = 100f;   // distance between repeating sections

    private Transform mainCam;
    private GameManager gameManager;     // reference to GameManager

    void Start()
    {
        // Find main camera
        if (Camera.main != null)
        {
            mainCam = Camera.main.transform;
            Debug.Log("Camera found: " + mainCam.name);
        }
        else
        {
            Debug.LogError("No MainCamera found! Tag your camera as 'MainCamera'");
        }

        // Find the GameManager
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void Update()
    {
        // Move this section downward every frame
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // When this section moves far below the camera...
        if (transform.position.y < mainCam.position.y - sectionHeight)
        {
            // ...move it above the other section
            transform.position += Vector3.up * sectionHeight * 2f;

            // Tell the GameManager that we reached a new level
            if (gameManager != null)
            {
                gameManager.NextLevel(); 
            }
        }
    }
}
