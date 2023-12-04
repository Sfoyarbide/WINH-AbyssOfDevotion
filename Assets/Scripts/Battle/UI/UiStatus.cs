using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiStatus : MonoBehaviour
{
    public Player player;
    public GameObject Menu_0;
    public GameObject ReturnToMainMenu;
    public PlayerNavagatorSystem PNS;
    public BattleManager BM;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        BM = FindObjectOfType<BattleManager>();
        PNS = FindObjectOfType<PlayerNavagatorSystem>();
    }

    public void OpenStatus()
    {
        PNS.OpenStatus();
    }

    public void UpdateStats()
    {
        BM.AM.Play("Confirmation");
        Menu_0.transform.GetChild(2).gameObject.GetComponent<Text>().text = "Lv:  " + player.Lv.ToString(); // Lvl
        Menu_0.transform.GetChild(3).gameObject.GetComponent<Text>().text = "ExpRest:  " + (player.ExLeftToNextlvl - player.Ex).ToString(); // Next Lvl
        Menu_0.transform.GetChild(4).gameObject.GetComponent<Text>().text = "Hp:  " + player.Hp.ToString(); // Hp
        Menu_0.transform.GetChild(5).gameObject.GetComponent<Text>().text = "Sp:  " + player.Sp.ToString(); // Sp
        Menu_0.transform.GetChild(7).gameObject.GetComponent<Text>().text = "MEDICINE: " + player.Medicide.ToString(); // Medicine     
    }

    public void UseMedicine()
    {
        if(player.Medicide != 0)
        {
            player.Medicide--;
            player.Hp += 25;
            BM.AM.Play("Confirmation");

            if(player.Hp >= player.HpMax)
            {
                player.Hp = player.HpMax;
            }
            UpdateStats();
        }
    }

    public void ReturnMainMenu()
    {
        BM.AM.StopAll();
        SceneManager.LoadScene(0);
    }
}
