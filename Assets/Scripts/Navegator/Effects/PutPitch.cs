using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPitch : MonoBehaviour
{
    public string ClipName;
    public bool IsRandom;
    public float putPitchValue;
    public AudioManager AM;

    private void Start() 
    {
        AM = FindObjectOfType<AudioManager>();
    }

    public void putPitch()
    {
        if(IsRandom)
        {
            AM.RandomizePitch(ClipName);
            Destroy(this.gameObject);
        }
        if(!IsRandom)
        {
            AM.PutPitch(ClipName, putPitchValue);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        putPitch();   
    }
}
