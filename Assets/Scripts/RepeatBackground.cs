using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
private Vector3 startPos;
    private float repeatWidth;

    void Start()
    {
        // Record the starting position
        startPos = transform.position;

        // Get half the width of the background’s collider
        repeatWidth = GetComponent<BoxCollider>().size.y / 2;
    }

    void Update()
    {
        // If the background has moved down past the repeat point, reset it to start
        if (transform.position.y < startPos.y - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
