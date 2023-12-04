using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetIS : MonoBehaviour
{
    public Player player;
    public int index;
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        player.IndexStorys = index;
    }
}
