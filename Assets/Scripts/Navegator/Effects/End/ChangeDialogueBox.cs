using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDialogueBox : MonoBehaviour
{
    public bool ChangeFirst;
    public Image[] DialogueGO;
    public Sprite sprite;

    void ChangeVisualForm()
    {
        if(ChangeFirst)
        {
            DialogueGO[0].color = new Color(0,0,0,0);
        }
        DialogueGO[1].sprite = sprite;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        ChangeVisualForm();
    }
}
