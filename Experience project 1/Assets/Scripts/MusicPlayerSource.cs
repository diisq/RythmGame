using UnityEngine;

public class MusicPlayerSource : MonoBehaviour
{
    public AudioSource audioSource; // Drag your AudioSource here in the inspector
    public float startTimeInSeconds = 5f; // Set this to the time you want the song to start playing

    void Start()
    {
        // Ensure the AudioSource is stopped initially
        audioSource.Stop();
        
        // Set the time to the desired start time in seconds
        audioSource.time = startTimeInSeconds;

        // Play the audio from the set time
        audioSource.Play();
    }
}