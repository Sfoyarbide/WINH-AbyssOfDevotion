using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputTester : MonoBehaviour
{
    public BattleManager BM;
    public bool TEMP;

    // Update is called once per frame
    void Update()
    {
        BM = FindObjectOfType<BattleManager>();
        updateTest(); // of using the test mode quit this comment. 
    }

    void updateTest()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            TEMP = !TEMP;
        }
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene(1);
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            SceneManager.LoadScene(2);
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene(3);
        }
        if(Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene(4);
        }
        if(Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene(5);
        }
        if(Input.GetKeyDown(KeyCode.F6))
        {
            BM.player.Ex = BM.player.ExLeftToNextlvl;
            BM.Lvlup();
        }
    }
}
