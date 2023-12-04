using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrackTrigger : MonoBehaviour
{
    BattleManager BM;
    public string Song;

    private void Start() 
    {
        BM = FindObjectOfType<BattleManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        BM.AM.Play(Song);
    }

    private void OnTriggerExit2D(Collider2D other) {
        Destroy(this.gameObject);
    }
}
