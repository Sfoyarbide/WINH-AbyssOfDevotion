using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fusible : MonoBehaviour
{
    public Player player;
    public PlayerNavagatorSystem PNS;
    public bool IsOn;
    public int indexCard;
    public bool CanBeOpen;

    private void Update() 
    {
        CheckPlayerDistance();
        UseFusible();
    }

    private void Start() 
    {
        player = GameObject.FindObjectOfType<Player>();
        PNS = player.GetComponent<PlayerNavagatorSystem>();
    }

    void CheckPlayerDistance()
    {
        if(Vector3.Distance(gameObject.transform.position, player.gameObject.transform.position) <= 1)
        {
            CanBeOpen = true;
        }
        else
        {
            CanBeOpen = false;
        }
    }

    void UseFusible()
    {
        //if(player.HaveKey[indexCard] && CanBeOpen && Input.GetKeyDown(KeyCode.E)) 
        if(player.HaveKey[indexCard] && CanBeOpen && Input.touchCount > 0 && !PNS.IsMoving) 
        {
            IsOn = true;
            FindObjectOfType<AudioManager>().Play("Confirmation");
        }
    }
}
