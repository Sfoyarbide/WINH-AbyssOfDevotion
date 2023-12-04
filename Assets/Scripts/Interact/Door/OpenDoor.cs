using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float Distance = 1; // 1 // 1.5f  
    PlayerNavagatorSystem player;
    BattleManager BM;
    public int indexCard;
    public bool CanBeOpen;
    public bool NeedKey;
    public bool NeedInput;
    public bool isOpening;

    private void Start() 
    {
        player = GameObject.FindObjectOfType<PlayerNavagatorSystem>();
        BM = GameObject.FindObjectOfType<BattleManager>();   
    }

    private void Update() 
    {
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
        if(NeedInput)
        {
            switch(NeedKey)
            {
                case false:
                    //if(CanBeOpen && Input.GetKeyDown(KeyCode.E) && !isOpening)
                    if(CanBeOpen && Input.touchCount > 0 && !isOpening) 
                    {
                        StartCoroutine(DestroyTimer());
                        isOpening = true;
                    }
                    break;
                case true:
                    //if(CanBeOpen && Input.GetKeyDown(KeyCode.E) && player.GetComponent<Player>().HaveKey[indexCard] && !isOpening) 
                    if(CanBeOpen && Input.touchCount > 0 && player.GetComponent<Player>().HaveKey[indexCard] && !isOpening) 
                    {
                        StartCoroutine(DestroyTimer());
                        isOpening = true;
                    }
                    break;
            }
        }
        else
        {
            StartCoroutine(DestroyTimer());           
        }
    }

    IEnumerator DestroyTimer()
    {
        BM.AM.Play("OpeningDoor");
        gameObject.GetComponent<Animator>().SetBool("CanOpen", true);
        yield return new WaitForSecondsRealtime(1.0f);
        Destroy(this.gameObject);
    }
}