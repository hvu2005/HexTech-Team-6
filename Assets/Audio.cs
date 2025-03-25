using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;
    public AudioClip musiClip;
    void Start()
    {
        musicAudioSource.clip = musiClip;
        musicAudioSource.Play();
    }
}