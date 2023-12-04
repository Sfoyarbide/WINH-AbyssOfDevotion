using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreas : MonoBehaviour
{
    public DialogueManager DM;
    public GameObject SaveOrLoad;
    public Player player;
    public DialogueTrigger[] AllDialogueTrigers;   

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();  
        DM = GameObject.FindObjectOfType<DialogueManager>();      
    }

    public void SavePlayerButton()
    {
        // Rest
        player.InTheSaveZone[DM.DS.IndexSafeZone] = true;
        AllDialogueTrigers[player.IndexStorys].TriggerDialogue(false);
        player.Hp = player.HpMax;
        player.Sp = player.SpMax;
        player.IndexStorys++;
        player.SavePlayerData("/playerData.dat");
        SaveOrLoad.SetActive(false);
        DM.DS.IsAlreadyRead = true;
    }

    public void CancelButton()
    {
        DM.IsSafeZone = false;
        DM.EndDialogue();
        
        SaveOrLoad.SetActive(false);
    }
}