using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundExtensions : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundFile( AudioClip sound, float volume = 1f)
    {
        audioSource.PlayOneShot(sound, volume);
    }
    public void PlaySoundFile( AudioClip sound)
    {
        audioSource.PlayOneShot(sound, 1f);
    }
}
