using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    bool CanUp;
    public Player player;
    public Text text;
    public Image image;
    public TraslatePlayerData TPD;

    private void Start() 
    {
        player = this.GetComponent<Player>();
        TPD = GameObject.FindObjectOfType<TraslatePlayerData>();
        player.LoadPlayerData(false, false, "/playerData.dat");
        TPD.IsLoadGame = false;
        if(player.Scene == 0)
        {
            text.text = "X";
        }
    } 

    private void Update() 
    {
        Upping();
    }

    public void LoadLastPlayerScene()
    {
        if(player.Scene != 0)
        {
            TPD.IsLoadGame = true;
            image.gameObject.SetActive(true);
            // Anim
            StartCoroutine(IsLoading(player.Scene));
        }
    }

    public void Exit()
    {
        StartCoroutine(IsLoading(0));
    }
    
    void Upping()
    {   
        if(CanUp)
            image.color += new Color(0.5f / Time.deltaTime, 0.5f / Time.deltaTime, 0.5f / Time.deltaTime, (3 * Time.deltaTime));
    }

    IEnumerator IsLoading(int index)
    {
        CanUp = true;
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene(index);
    }
}
