using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadScreen : MonoBehaviour
{
    public BattleManager BM;
    public bool canShow;
    SpriteRenderer SR;

    private void Start() 
    {
        BM = GameObject.FindObjectOfType<BattleManager>();
        SR = GetComponent<SpriteRenderer>();
    }

    private void Update() 
    {
        Dead_effect();
        CanShow();
    }

    void Dead_effect()
    {
        if(BM.IsGameOver)
        {
            BM.AM.Play("GameOver");
            StartCoroutine(ShowScreenDead());
        }
    }

    IEnumerator ShowScreenDead()
    {
        yield return new WaitForSecondsRealtime(3f);
        canShow = true;
    }

    void CanShow()
    {
        if(canShow)
        {
            BM.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            BM.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            SR.color += new Color(0,0,0,(3 * Time.deltaTime) / 2);
            StartCoroutine(LoadScene());
        }
    }

    IEnumerator LoadScene()
    {
        yield return new WaitForSecondsRealtime(4.5f);
        SceneManager.LoadScene("GameOver");
    }
}
