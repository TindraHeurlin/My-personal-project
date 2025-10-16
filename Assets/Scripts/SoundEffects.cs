using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    private AudioSource audioSource;

    [Header("Sound Clips")]
    public AudioClip bounceSound;
    public AudioClip powerUpSound;
    public AudioClip gameOverSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayBounce()
    {
        audioSource.PlayOneShot(bounceSound);
    }

    public void PlayPowerUp()
    {
        audioSource.PlayOneShot(powerUpSound);
    }

    public void PlayGameOver()
    {
        audioSource.PlayOneShot(gameOverSound);
    }
}
