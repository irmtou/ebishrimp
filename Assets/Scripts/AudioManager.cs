using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance
    public static AudioManager Instance { get; private set; }

    [Header("Audio Source")]
    public AudioSource audioSource;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }

        // Ensure the audio source is set
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Play a specific audio clip
    public void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    // Stop currently playing audio
    public void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
