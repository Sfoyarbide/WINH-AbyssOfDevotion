using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTrainDoor : MonoBehaviour
{
    public float Distance = 1.5f;
    PlayerNavagatorSystem player;
    public GameObject MoveToHere;
    public bool Stairs;
    public bool CanBeOpen;
    public bool AlreadyEnter;
    public bool CanUp;
    public SpriteRenderer BlackThing;
    public Transform toTeleport;
    
    private void Start() 
    {
        player = GameObject.FindObjectOfType<PlayerNavagatorSystem>();   
    }

    private void Update() 
    {
        UpBlackThing();
        CheckPlayerDistance();
        OpenDoor_input();
    }

    void CheckPlayerDistance()
    {
        if(Vector3.Distance(gameObject.transform.position, player.gameObject.transform.position) <= Distance)
        {
            CanBeOpen = true;
        }
        else
        {
            CanBeOpen = false;
        }
    }

    void OpenDoor_input()
    {   
        //if(CanBeOpen && Input.GetKeyDown(KeyCode.E) && player.GetComponent<Player>().HaveKey[0] && !AlreadyEnter)
        if(CanBeOpen && Input.touchCount > 0 && player.GetComponent<Player>().HaveKey[0] && !AlreadyEnter) 
        {
            StartCoroutine(TeleportTimer());
            AlreadyEnter = true;
        }
    }

    IEnumerator TeleportTimer()
    {
        player.CanMove = false;
        CanUp = true;

        if(!Stairs)
        {
            player.BM.AM.Play("Train");
        }

        yield return new WaitForSecondsRealtime(5.0f);

        player.transform.position = toTeleport.position;
        MoveToHere.transform.position = toTeleport.position;

        yield return new WaitForSecondsRealtime(0.5f);
        CanUp = false;

        player.CanMove = true;
        Destroy(BlackThing.gameObject);
        Destroy(this.gameObject);
    }

    void UpBlackThing()
    {
        if(CanUp)
            BlackThing.color += new Color(0,0,0, 3f * Time.deltaTime);
    }
}
