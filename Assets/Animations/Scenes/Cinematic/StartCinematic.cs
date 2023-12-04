using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCinematic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame_effect());
    }

    IEnumerator StartGame_effect()
    {
        yield return new WaitForSecondsRealtime(15f);
        SceneManager.LoadScene("LvL1");
    }
}