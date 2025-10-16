using UnityEngine;

public class RotatePowerUp : MonoBehaviour
{
    [Header("Visual Settings")]
    public float rotateSpeed = 90f;      // how fast it spins
    public float floatAmplitude = 0.25f; // how far it moves up/down
    public float floatFrequency = 2f;    // how fast it floats

    private Vector3 startPos;

    void Start()
    {
        // Save starting position so we can move smoothly up/down
        startPos = transform.position;
    }

    void Update()
    {
        //Spin around its front axis (so it’s visible in 2D view)
        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);

        //Add a gentle float motion
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}
