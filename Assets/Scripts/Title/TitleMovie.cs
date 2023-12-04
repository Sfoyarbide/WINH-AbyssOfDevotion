using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class TitleMovie : MonoBehaviour
{
    public Button[] allButtons;
    public Player player;
    public AudioManager AM;
    public GameObject Object;
    public Image image;
    public Animator[] effects;
    public Animator[] EffectsLoad;
    public TraslatePlayerData TPD;

    private void Start() 
    {
        StartCoroutine(Orden());
        player = this.GetComponent<Player>();
        TPD = GameObject.FindObjectOfType<TraslatePlayerData>();
        player.LoadPlayerData(false, false, "/playerData.dat"); 
        if(player.Scene == 0)
        {
            image.color = new Color(0,0,0,255);
            image.GetComponent<Button>().enabled = false;
        }
        AM = FindObjectOfType<AudioManager>();
    }

    private void Update() 
    {
        if(Input.anyKeyDown)
        {
            effects[2].SetBool("move", true);
        }
    }

    IEnumerator Orden()
    {
        yield return new WaitForSecondsRealtime(2f);
        
        effects[0].SetBool("move", true);

        yield return new WaitForSecondsRealtime(13f);

        effects[1].SetBool("move", true);

        yield return new WaitForSecondsRealtime(10f);

        effects[2].SetBool("move", true);
    }

    public void NewGame()
    {
        StartCoroutine(NewGameEffect());
        DisableAllButtons();
    }

    public void ExitGame()
    {
        StartCoroutine(ExitEffect());
        DisableAllButtons();
    }

    IEnumerator NewGameEffect()
    {
        Debug.Log("effects");
        AM.Play("Confirmation");
        yield return new WaitForSecondsRealtime(0.02f);
        DeleteBeforeFiles();
        EffectsLoad[0].SetBool("move", true);
        yield return new WaitForSecondsRealtime (10f);
        SceneManager.LoadScene("LvL1");
    }

    void DisableAllButtons()
    {
        allButtons[0].enabled = false;
        allButtons[1].enabled = false;
        allButtons[2].enabled = false;
    }

    void DeleteBeforeFiles()
    {
        if(File.Exists("SaveData/PlayerData.dat"))
        {
            File.Delete("SaveData/PlayerData.dat");
        }
        if(File.Exists("SaveData/playerTemp.dat"))
        {
            File.Delete("SaveData/playerTemp.dat");
        }
    }

    public void LoadLastPlayerScene()
    {
        DisableAllButtons();
        AM.Play("Confirmation");
        TPD.IsLoadGame = true;
        EffectsLoad[1].SetBool("load", true);
        // Anim
        StartCoroutine(IsLoading());
    }

    IEnumerator IsLoading()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(player.Scene);
    }

    IEnumerator ExitEffect()
    {
        EffectsLoad[1].SetBool("load", true);    
        yield return new WaitForSecondsRealtime(3f);
        Application.Quit();
    }
}