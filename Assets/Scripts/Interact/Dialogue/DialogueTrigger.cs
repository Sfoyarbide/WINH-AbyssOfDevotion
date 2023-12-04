using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogueStart DS;
    public bool IsSafeZone;
    public Dialogue dialogue;

    private void Start() 
    {
        DS = gameObject.GetComponent<DialogueStart>();
    }

    public void TriggerDialogue(bool notUseMusic)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, IsSafeZone, DS, notUseMusic);
    }
}
