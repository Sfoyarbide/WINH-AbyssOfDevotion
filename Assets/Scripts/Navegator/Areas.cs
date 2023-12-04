using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Areas : MonoBehaviour
{
    public EnemyPresence EP;
    public NavegationSystem NS;
    public Enemy[] EnemiesInThisArea;
    public bool CanActivePresence;
    public bool NotPlayMusic;

    private void Start() 
    {
        NS = GameObject.Find("Level").GetComponent<NavegationSystem>();
        EP = GameObject.FindObjectOfType<EnemyPresence>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(!NS.BM.IsBattle)
        {
            NS.indexArea = int.Parse(this.gameObject.name);
            NS.CanEnterBattle = true;
            Debug.Log(int.Parse(this.gameObject.name));
            if(!CanActivePresence)
            {
                EP.status = Status.PRESENCE;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(!NS.BM.IsBattle)
        {
            NS.CanEnterBattle = false;
            if(!CanActivePresence)
            {
                EP.status = Status.NOPRESENCE;
            }
        }
    }
}
