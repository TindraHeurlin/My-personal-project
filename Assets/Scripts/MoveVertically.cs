using UnityEngine;

public class MoveVertically : MonoBehaviour
{
    public float scrollSpeed = 1f;

    void Update()
    {
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
    }
}
