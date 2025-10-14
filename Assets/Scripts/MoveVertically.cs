using UnityEngine;

public class MoveVertically : MonoBehaviour
{
    public float scrollSpeed = 2f;

    void Update()
    {
        transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
    }
}
