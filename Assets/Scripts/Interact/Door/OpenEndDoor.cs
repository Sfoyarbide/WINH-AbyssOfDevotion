using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenEndDoor : MonoBehaviour
{
    public float Distance;
    PlayerNavagatorSystem player;
    BattleManager BM;
    public bool CanBeOpen;
    public Fusible[] checkGenerator;
    
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
        //if(CanBeOpen && Input.GetKeyDown(KeyCode.E) && checkGenerator[0].IsOn)
        if(CanBeOpen && Input.touchCount > 0 && checkGenerator[0].IsOn)
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
