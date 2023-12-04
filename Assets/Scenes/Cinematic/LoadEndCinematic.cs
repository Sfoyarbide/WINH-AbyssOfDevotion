using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndCinematic : MonoBehaviour
{
    public BattleManager BM;

    private void Start() 
    {
        BM = FindObjectOfType<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckBattle();
    }

    void CheckBattle()
    {
        if(BM.IsBattleFinish)
        {
            SceneManager.LoadScene("EndCinematic");
            BM.AM.StopAll();
        }
    }
}
