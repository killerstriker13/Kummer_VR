using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource audioSource;


    public void Play(int r)
    {
        audioSource.clip = audioClips[r-1];
        audioSource.Play();
    }
}
