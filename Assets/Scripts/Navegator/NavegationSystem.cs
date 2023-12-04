using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavegationSystem : MonoBehaviour
{
    public bool notStartPlaying;
    public AudioManager AM;
    public LvlManager LM;
    public GameObject UI;
    public bool UseTrasitionLoad;
    public EnemyPresence EP;
    public Areas[] areas;
    public int indexArea;
    public GameObject[] Cameras;
    Animator Background;
    public BattleManager BM;
    public GameObject BMob;
    public UiBattleManager UIBM;
    public PlayerNavagatorSystem Player;
    public int fail = 0;
    public TraslatePlayerData TPD;
    public GameObject UIBattle;
    public GameObject EnemyAmbush;
    public GameObject BossAmbush;
    public SpriteRenderer SpecialSprite;
    public Player player;
    public bool AlreadyCreate = false;
    public bool CanEnterBattle = false;
    public int Indexlevel;
    public MobileInput MI;
    bool CanUp;

    private void Awake() 
    {
        Player = GameObject.FindObjectOfType<PlayerNavagatorSystem>();
        player = GameObject.FindObjectOfType<Player>();
        player.Scene = Indexlevel;
        TPD = GameObject.FindObjectOfType<TraslatePlayerData>();
        if(TPD.IsLoadGame)
        {
            player.LoadPlayerData(true, true, "/playerData.dat");
            TPD.IsLoadGame = false;    
        }
        else if(UseTrasitionLoad) 
        {
            player.LoadPlayerData(false, false, "/playerTemp.dat");
        }
    }

    private void Start() 
    {
        LM = GameObject.FindObjectOfType<LvlManager>();
        MI = FindObjectOfType<MobileInput>();
        EnemyAmbush = player.gameObject.transform.GetChild(2).gameObject;
        BossAmbush = player.gameObject.transform.GetChild(3).gameObject;
        UIBattle = BM.gameObject.transform.GetChild(0).gameObject;
        EP = GameObject.FindObjectOfType<EnemyPresence>();
        Background = BM.transform.GetChild(5).GetComponent<Animator>();
        UI = GameObject.Find("UI");
        LM.CheckLvL(player);
        AM = FindObjectOfType<AudioManager>();
        if(!notStartPlaying)
        {
            AM.Play("ExploringSong");
        }
    }

    private void Update() 
    {
        Upping();
    }

    public void InitBattle()
    {
        int Dice = UnityEngine.Random.Range(10,20);
        if(fail >= Dice && !AlreadyCreate)
        {
            // Effect
            fail = 0;
            StartCoroutine(EnterInBattle(EnemyAmbush, "BattleSong"));
            EP.status = Status.NOTHING;
            UI.transform.GetChild(0).gameObject.SetActive(false);
            UI.transform.GetChild(8).gameObject.SetActive(false);

            // Init
            Player.CanMove = false;
            int DiceEnemy = UnityEngine.Random.Range(0, areas[indexArea].EnemiesInThisArea.Length); 

            // Assing Enemy
            GameObject Enemy = Instantiate(areas[indexArea].EnemiesInThisArea[DiceEnemy].gameObject, new Vector3(7f,3f,0f), Quaternion.identity);
            BM.enemy = Enemy.GetComponent<Enemy>();
            BM.enemy.gameObject.name = BM.enemy.Name;
            BM.enemy.gameObject.SetActive(true);

            // Setting 
            BMob.SetActive(true);
            BM.IsBattle = true;
            AlreadyCreate = true;
            BM.BattleText.text = "Enemy  Ambush!,  esperando  ataque  de  Atenea.";
        }
        else
        {
            fail++;
        }
    }

    public void InitBossBattle(Enemy enemy, bool IsNotNormalBoss)
    {
        // Effects
        if(!IsNotNormalBoss)
        {
            StartCoroutine(EnterInBattle(BossAmbush, "BattleSong"));
        }
        else if(IsNotNormalBoss)
        {
            StartCoroutine(EnterInBattleBoss_ending("BattleSong"));
        }

        EP.status = Status.NOTHING;
        UI.transform.GetChild(0).gameObject.SetActive(false);
        UI.transform.GetChild(8).gameObject.SetActive(false);

        // Init
        Player.CanMove = false;

        // Assing Enemy
        GameObject Enemy = Instantiate(enemy.gameObject, new Vector3(7f,3f,0f), Quaternion.identity);
        BM.enemy = Enemy.GetComponent<Enemy>();
        BM.enemy.gameObject.name = BM.enemy.Name;
        BM.enemy.gameObject.SetActive(true);

        // Setting 
        BMob.SetActive(true);
        BM.IsBattle = true;
        AlreadyCreate = true;
        BM.BattleText.text = "Boss  Ambush!, esperando  a  Atenea.";
    }

    public void ReturnToNav()
    {
        TurnOnButton(); // pc
        EP.status = Status.PRESENCE;
        UI.transform.GetChild(0).gameObject.SetActive(true); // pc
        UI.transform.GetChild(8).gameObject.SetActive(true); 
        UIBM.BattleStatus.SetBool("IsBattleFinish", false);
        UIBM.SkillButton.SetActive(true);
        BMob.SetActive(false);
        AlreadyCreate = false;
        UIBattle.SetActive(false);
        BM.IsBattle = false;

        Background.gameObject.transform.position = new Vector3(-60,0,0);
        UIBM.playerGO.GetComponent<Animator>().SetBool("CanOpen", false);
        BM.InPlayerTurn = true;
        BM.IAisfinish = false;
        BM.InAction = false;

        Destroy(BM.enemy.gameObject);
        Cursor.lockState = CursorLockMode.Locked;
        Player.CanMove = true;
        BM.BattleFinishAnim.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,550);

        if(!areas[indexArea].NotPlayMusic)
        {
            BM.AM.Play("ExploringSong");
        }
    }

    public IEnumerator EnterInBattle(GameObject WhatAmbush, string BattleTrack)
    {
        AM.Pause("ExploringSong");
        yield return new WaitForSecondsRealtime(0.1f);

        AM.Play("EnterBattle");
        WhatAmbush.SetActive(true);
        WhatAmbush.GetComponent<Animator>().SetBool("IsAmbush", true);   

        yield return new WaitForSecondsRealtime(2f);

        WhatAmbush.GetComponent<Animator>().SetBool("IsAmbush", false);
        WhatAmbush.SetActive(false);
        yield return new WaitForSecondsRealtime(0.05f);

        Cameras[0].SetActive(false); // Nav Cam
        Cameras[1].SetActive(true); // Battle Cam


        yield return new WaitForSecondsRealtime(0.05f);
        Background.SetBool("show", true);
        AM.Play(BattleTrack);

        yield return new WaitForSecondsRealtime(0.05f);
        
        Cursor.lockState = CursorLockMode.Confined;
        UIBattle.SetActive(true);
    }

    public IEnumerator EnterInBattleBoss_ending(string BattleTrack)
    {
        AM.Pause("ExploringSong");
        yield return new WaitForSecondsRealtime(0.1f);

        AM.Play("EnterBattle");
        CanUp = true;
        yield return new WaitForSecondsRealtime(2f);

        CanUp = false;
        yield return new WaitForSecondsRealtime(0.05f);

        Cameras[0].SetActive(false); // Nav Cam
        Cameras[1].SetActive(true); // Battle Cam


        yield return new WaitForSecondsRealtime(0.05f);
        Background.SetBool("show", true);
        BM.AM.Play(BattleTrack);

        yield return new WaitForSecondsRealtime(0.05f);
        
        Cursor.lockState = CursorLockMode.Confined;
        UIBattle.SetActive(true);
    }

    public void Upping()
    {
        if(CanUp)
        {
            SpecialSprite.color += new Color(0,0,0,2 * Time.deltaTime);
        }
        else
        {
            SpecialSprite.color = new Color(255,255,255,0);
        }
    }

    // Mobile
    void TurnOnButton()
    {
        for(int x = 0; x < MI.buttonsMobile.Length; x++)
        {
            MI.buttonsMobile[x].gameObject.SetActive(true);
            MI.PNS.x = 0;
            MI.PNS.y = 0;
        }
    }
}