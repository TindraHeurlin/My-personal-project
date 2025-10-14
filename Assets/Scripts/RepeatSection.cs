using UnityEngine;

public class RepeatSection : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float sectionHeight = 100f; // distance between A and B
    public float lowerLimit = -50f;    // when to recycle
    private Transform mainCam;

    void Start()
    {
       if (Camera.main != null)
    {
        mainCam = Camera.main.transform;
        Debug.Log(" Camera found: " + mainCam.name);
    }
    else
    {
        Debug.LogError(" No MainCamera found! Tag your camera as 'MainCamera'");
    }
        
    }

    void Update()
    {
        // Move the section downward every frame
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);

        // If this section moves far below the camera, teleport it above the other one
        if (transform.position.y < mainCam.position.y - sectionHeight)
        {
            transform.position += Vector3.up * sectionHeight * 2f;
        }
    }
}
