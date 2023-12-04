using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutPause : MonoBehaviour
{
    public string ClipName;
    public AudioManager AM;
    public bool NotWillDestroy;

    private void Start() 
    {
        AM = FindObjectOfType<AudioManager>();
    }

    public void putPause()
    {
        AM.Pause(ClipName);
        if(!NotWillDestroy)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        putPause();   
    }
}
