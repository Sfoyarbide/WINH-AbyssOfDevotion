using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string LvL;  // LvL1...
    public SpriteRenderer BlackThing;
    public AudioManager AM;
    public bool CanUp;
    Player player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        AM = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        player.SavePlayerData("/playerTemp.dat");
        CanUp = true;
        AM.StopAll();
        yield return new WaitForSecondsRealtime(3);
        SceneManager.LoadScene(LvL);
    }

    private void Update() 
    {
        UpBlackThing();
    }

    void UpBlackThing()
    {
        if(CanUp)
            BlackThing.color += new Color(0,0,0, 3f * Time.deltaTime);
    }
}