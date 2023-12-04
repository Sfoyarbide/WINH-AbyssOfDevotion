using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ChangeVideo : MonoBehaviour
{
    public GameObject TheEnd;
    public GameObject image;
    public VideoPlayer[] VP;
    DialogueManager DM;
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
        Debug.Log(DM.sentences.Count);
        if(DM.sentences.Count == 5 && DM.IsTalking)
        {
            image.SetActive(true);
            VP[0].Play();
        }
        if(DM.sentences.Count == 3 && DM.IsTalking)
        {
            VP[0].Stop();
            VP[1].Play();
        }
        if(DM.sentences.Count == 0 && DM.IsTalking)
        {
            StartCoroutine(theEnd());
        }
    }

    IEnumerator theEnd()
    {
        yield return new WaitForSecondsRealtime(5f);
        DM.DialogueBox.gameObject.SetActive(false);
        TheEnd.SetActive(true);
        yield return new WaitForSecondsRealtime(15f);
        SceneManager.LoadScene("Credits");
    }
}
