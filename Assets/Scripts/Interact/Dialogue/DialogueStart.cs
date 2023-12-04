using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStart : MonoBehaviour
{
    public bool notUseMusic;
    public EnemyPresence EP;
    public Player player;
    public PlayerNavagatorSystem PNS;
    BattleManager BM;
    public GameObject IncomingCall;
    public DialogueManager DM;
    public DialogueTrigger dialogueTrigger;
    public float[] decimalLvL;
    public bool ItIsInThisObject;
    public bool IsInstance, IsAlreadyRead, SI;
    public bool IsSafeZone;
    public int IndexSafeZone;
    public bool CanAnswer;
    public bool AlreadyCall;
    public bool NotExploringSong;
    public AnswerCall AC;
    private void Start() 
    {
        player = GameObject.FindObjectOfType<Player>();
        PNS = GameObject.FindObjectOfType<PlayerNavagatorSystem>();
        EP = GameObject.FindObjectOfType<EnemyPresence>();
        BM = FindObjectOfType<BattleManager>();
        if(ItIsInThisObject == true)
        {
            dialogueTrigger = this.gameObject.GetComponent<DialogueTrigger>();
        }
        DM = FindObjectOfType<DialogueManager>();
        AC = FindObjectOfType<AnswerCall>();
    }

    private void Update() 
    {
        if(IsSafeZone)
        {
            CallImcomingSave_System();
        }
    }

    public void StartDialogue()
    {
        if(!notUseMusic)
        {
            DM.PNS.BM.AM.Play("MisterySong");
        }
        dialogueTrigger.TriggerDialogue(notUseMusic);
        SI = false;
        if(!IsSafeZone)
            IsAlreadyRead = true;
    }

    public IEnumerator CallIncoming()
    {
        // Put sound
        if(IsInstance)
        {
            IsAlreadyRead = true;
        }

        if(!notUseMusic)
        {
            StartCoroutine(DM.PNS.BM.AM.QuitVolumeSlow("ExploringSong"));
        }
        
        BM.AM.Play("Ringtone");
        AlreadyCall = true;
        IncomingCall.SetActive(true);
        yield return new WaitForSeconds(1f);
        IncomingCall.SetActive(false);
        yield return new WaitForSeconds(1f);
        BM.AM.Play("Ringtone");
        IncomingCall.SetActive(true);
        yield return new WaitForSeconds(1f);
        IncomingCall.SetActive(false);

        if(IsInstance)
        {
            StartDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        CanAnswer = false;
        AlreadyCall = false;
        if(IsSafeZone)
        {
            EP.status = Status.NOPRESENCE;
            StopAllCoroutines();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        AC.DS = this;
        if(IsInstance && !IsAlreadyRead && !IsSafeZone)
        {
            DM.UI.transform.GetChild(7).gameObject.SetActive(false);
            PNS.CanMove = false;
            StartCoroutine(CallIncoming());
        }
        if(!IsInstance && !IsAlreadyRead && !IsSafeZone)
        {
            StartCoroutine(CallIncoming());
            if(Input.GetKey(KeyCode.E))
            {
                StartDialogue();
            }
        }
        if(player.InTheSaveZone[IndexSafeZone] == false && IsSafeZone && !IsAlreadyRead)
        {
            CanAnswer = true;
            EP.status = Status.SAFEZONE;
        }
    }

    public void CallImcomingSave_System()
    {
        if(CanAnswer && player.InTheSaveZone[IndexSafeZone] == false)
        {
            Debug.Log(IsStatic());
            if(Input.GetKeyDown(KeyCode.E) && IsStatic())
            {
                StartDialogue();
            }
            if(!AlreadyCall)
            {
                StartCoroutine(CallIncoming());
            }
        }
    }

    bool IsStatic()
    {
        float x = player.transform.position.x;
        float y = player.transform.position.y;

        if(Mathf.Abs(x) == Mathf.Abs(((int)x) + decimalLvL[0]) && Mathf.Abs(y) == Mathf.Abs(((int)y) + decimalLvL[1])) 
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
