using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Briefcase : MonoBehaviour
{
    public BriefcaseEffect BE;
    public BattleManager BM;
    public Player player;
    public PlayerNavagatorSystem PNS;
    public bool[] Things; // put other things
    public int indexCard;
    public bool canUse;
    public bool AlreadyUse;

    private void Start() 
    {
        player = GameObject.FindObjectOfType<Player>();
        BM = GameObject.FindObjectOfType<BattleManager>();
        BE = GameObject.FindObjectOfType<BriefcaseEffect>();
        PNS = player.GetComponent<PlayerNavagatorSystem>();
    }

    private void Update() 
    {
        CheckPlayerDistance();
        //if(Input.GetKeyDown(KeyCode.E) && canUse && BM.IsBattle == false && !AlreadyUse)
        if(Input.touchCount > 0 && canUse && BM.IsBattle == false && !AlreadyUse) 
        {
            BM.AM.Play("OpeningBriefcase");
            if(Things[0])
            {
                player.Medicide++;             
                // Do Anim
                Idontknowisthisworks(3,this.gameObject);
            }
            if(Things[1])
            {
                Idontknowisthisworks(indexCard, this.gameObject);
                player.HaveKey[indexCard] = true;
                //Destroy(this.gameObject); 
            }
            AlreadyUse = true;
        }
    }

    void CheckPlayerDistance()
    {
        if(Vector3.Distance(gameObject.transform.position, player.gameObject.transform.position) <= 1.5f)
        {
            canUse = true;
        }
        else
        {
            canUse = false;
        }
    }

    void Idontknowisthisworks(int i, GameObject b)
    {
        StartCoroutine(BE.ShowPick(i,b));
    }
}
