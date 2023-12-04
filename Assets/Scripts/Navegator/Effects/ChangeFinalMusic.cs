using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeFinalMusic : MonoBehaviour
{
    public AudioClip finalMusic;
    public AudioManager AM;

    // Start is called before the first frame update
    void Start()
    {
        AM = FindObjectOfType<AudioManager>();
        AM.sounds[13].source.clip = finalMusic;
        AM.sounds[13].source.pitch = 1f;
    }
}
