using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerCall : MonoBehaviour
{
    public DialogueStart DS;

    public void answerCall()
    {
        DS.SI = true;
        DS.StartDialogue();
    }
}
