using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPresence : MonoBehaviour
{
    public EnemyPresence EP;

    private void Start() 
    {
        EP = GameObject.FindObjectOfType<EnemyPresence>();    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        EP.status = Status.BOSSPRESENCE;
    }
    
}
