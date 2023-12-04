using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Status
{
   NOTHING, NOPRESENCE, PRESENCE, SAFEZONE, BOSSPRESENCE
}

public class EnemyPresence : MonoBehaviour
{
    Image image;
    public Status status = new Status();
    public GameObject[] Presence;

    private void Start() 
    {
        image = this.GetComponent<Image>();
    }

    private void Update() 
    {
        CheckStatus();
    }

    void CheckStatus()
    {
        if(status == Status.NOPRESENCE)
        {
            Presence[0].SetActive(true);
            Presence[1].SetActive(false);
            Presence[2].SetActive(false);  
            Presence[3].SetActive(false);
        } 
        else if(status == Status.SAFEZONE)
        {
            Presence[0].SetActive(false);
            Presence[1].SetActive(false);
            Presence[2].SetActive(false);
            Presence[3].SetActive(true);
        }
        else if(status == Status.PRESENCE)
        { 
            Presence[0].SetActive(false);
            Presence[1].SetActive(true);
            Presence[2].SetActive(false);
            Presence[3].SetActive(false);         
        }
        else if(status == Status.BOSSPRESENCE)
        {
            Presence[0].SetActive(false);
            Presence[1].SetActive(false);
            Presence[2].SetActive(true);
            Presence[3].SetActive(false);
        }
        else if(status == Status.NOTHING)
        {
            Presence[0].SetActive(false);
            Presence[1].SetActive(false);
            Presence[2].SetActive(false);
            Presence[3].SetActive(false);
        }
    }
}
