using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource musicSource;   // Drag your AudioSource here
    public AudioClip backgroundMusic; // Drag your music clip here

    void Start()
    {
        // Assign the clip if not already set in Inspector
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }
}
