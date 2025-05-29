using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    BUTTON,
    WEAPONSWORD,
    WEAPONSTAFF,
    COLLECTPOINT,
    BUTTONCLICK,
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    private static SoundManager instance;
    private AudioSource audioSource;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1)
    {
        if (instance != null && (int)sound < instance.soundList.Length)
            instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }
}
