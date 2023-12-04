using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Properties")]
    public GameObject UI;
    public PlayerNavagatorSystem PNS;
    public Text nameText;
    public Text dialogueText;
    public Animator DialogueBox;
    public DialogueStart DS;
    public bool IsTalking;
    public bool IsSafeZone;
    public SafeAreas SA;
    public Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
        SA = GameObject.FindObjectOfType<SafeAreas>();
        PNS = GameObject.FindObjectOfType<PlayerNavagatorSystem>();
        UI = GameObject.Find("UI");
    }

    public void StartDialogue (Dialogue dialogue, bool isSafeZone, DialogueStart dialogueStart, bool notUseMusic)
    {
        if(dialogueStart != null)
        {
            DS = dialogueStart;
            Debug.Log(DS.gameObject.name);
        }

        if(notUseMusic)
        {
            PNS.BM.AM.StopAll();
        }
        
        IsSafeZone = isSafeZone;
        if(IsTalking == false)
        {
            // Unlock cursor
            Cursor.lockState = CursorLockMode.Confined;

            // QuitUI
            UI.transform.GetChild(7).gameObject.SetActive(false);

            // Start
            DialogueBox.SetBool("CanOpen", true);

            nameText.text = dialogue.name;
            sentences.Clear();

            foreach(string sentence in dialogue.sentences)
            {   
                sentences.Enqueue(sentence);
            }

            DisplayNextSentence();
            IsTalking = true;
            PNS.CanMove = false;
        }
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {   
        if(!IsSafeZone)
        {
            // PutUI
            UI.transform.GetChild(7).gameObject.SetActive(true);
            DialogueBox.SetBool("CanOpen", false);
            IsTalking = false;
            Cursor.lockState = CursorLockMode.Locked;
            PNS.CanMove = true;
            PNS.BM.AM.StopAll();
            if(!DS.NotExploringSong)
            {
                PNS.BM.AM.Play("ExploringSong");
            }
        }
        else
        {
            PNS.BM.AM.StopAll();
            SA.SaveOrLoad.SetActive(true);
            dialogueText.text = "";
            IsTalking = false;
        }
    }
}