using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLastBattle : MonoBehaviour
{
    DialogueManager DM;
    public DialogueStart DS; 
    public GameObject Button;
    // Start is called before the first frame update
    void Start()
    {
        DM = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckEverySentence();
    }

    void CheckEverySentence()
    {
        if(DM.sentences.Count == 13 && DM.IsTalking && DS.AlreadyCall)
        {
            Button.SetActive(false);
        }
        if(DM.sentences.Count == 0 && DS.AlreadyCall && DM.IsTalking && DS.IsAlreadyRead)
        {
            StartCoroutine(Wait());
        }
    }
    
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(1f);
        SceneManager.LoadScene("FinalBattle");
    }
}
