using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSoundMusic : MonoBehaviour
{
    public AudioClip backSoundClip; // Drag backsound clip di Inspector
    private AudioSource audioSource;
    [SerializeField, Range(0, 1)] private float volume = 1f; // Volume kontrol

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        audioSource.clip = backSoundClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        audioSource.volume = volume; // Set volume dari Inspector
        audioSource.Play();
    }
}
