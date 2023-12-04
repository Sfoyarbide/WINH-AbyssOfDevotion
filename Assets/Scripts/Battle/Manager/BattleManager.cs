using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    #region Vars
    public bool InPlayerTurn, IAisfinish, IsBattle, InAction, IsBattleFinish, IsGameOver;
    public Animator BattleFinishAnim;
    public IaBattleManager IBM;
    public NavegationSystem NS;
    public LvlManager lM;
    public Text BattleText;
    public GameObject playerGO;
    public GameObject[] EnemyEffects;
    public GameObject[] UIBattleResults;
    public int indexEffect;
    public int Index;
    public AudioManager AM;
    public Player player;
    public Enemy enemy;
    #endregion
    #region Init
    private void Start() 
    {
        lM = GameObject.FindObjectOfType<LvlManager>();
        NS = GameObject.FindObjectOfType<NavegationSystem>();
        IBM = FindObjectOfType<IaBattleManager>();
        player = GameObject.FindObjectOfType<Player>();
        playerGO = this.gameObject.transform.GetChild(1).gameObject;
        AM = GameObject.FindObjectOfType<AudioManager>();
    }

    private void Update() 
    {
        if(IsBattle)
        {
            CheakWin();
            CheakPlayerHealth();
        }
    }
    #endregion
    #region Ex System
    // Ex
    void ExGainInBattle()
    {
        int ExpGain;
        ExpGain = player.Lv * (enemy.Lv - player.Lv);
        if((enemy.Lv - player.Lv) <= 1)
        {
            ExpGain = enemy.Lv * 2;
        }
        player.Ex += ExpGain;

        UIBattleResults[1].GetComponent<Text>().text = ExpGain.ToString();
        BattleFinishAnim.SetBool("Dead", true);
        CheckLvLUp();
    }

    void CheckLvLUp()
    {
        if(player.Ex >= player.ExLeftToNextlvl)
        {
            UIBattleResults[2].SetActive(true);
            Lvlup();
        }
    }

    public void Lvlup()
    {
        player.Lv++;
        player.HpMax += 5;
        player.SpMax += 5;
        player.BaseDmg += 2;
        player.St += 2;
        player.En += 2;
        player.Ma += 2;
        player.Ag += 2;
        player.lu += 1;
        player.ExLeftToNextlvl = (player.HpMax + player.Lv) * player.Lv;
        if(player.Lv % 2 == 0 && player.Lv < 25)
        {
            UIBattleResults[3].SetActive(true);
            lM.CheckLvL(player);
        }
        if(player.Ex >= player.ExLeftToNextlvl)
        {
            Lvlup();
        }
    }

    #endregion
    #region Turn Manager
    // Turn Manager
    public void NextTurn()
    {
        InPlayerTurn = !InPlayerTurn;
        InAction = false;
        IAisfinish = false;
        if(InPlayerTurn)
        {
            NS.UIBM.AttackButton.SetActive(true);
            NS.UIBM.SkillButton.SetActive(true);
            NS.UIBM.medicineButton.SetActive(true);
            NS.UIBM.BattleStatus.SetBool("Go",false);
        }
    }

    void CheakWin()
    {
        if(!IsBattleFinish && enemy.Hp <= 0)
        {
            enemy.Hp = 0;
            StartCoroutine(FinishBattle_Effects());
        } 
        if(IsBattleFinish && Input.anyKeyDown)
        {
            BattleFinishAnim.gameObject.transform.GetChild(5).gameObject.SetActive(false);
            BattleFinishAnim.gameObject.transform.GetChild(6).gameObject.SetActive(false);
            NS.UIBM.BattleStatus.SetBool("IsBattleFinish", true);
            StartCoroutine(ReturnNavEffect());
        }
    }
    #endregion
    #region Game Over System
    public void CheakPlayerHealth()
    {
        if(player.Hp <= 0)
        {
            BattleText.text = "Atenea ha muerto!.";
            IsGameOver = true;
            AM.StopAll();
        }
    }
    
    #endregion
    #region Effects
    // Effects
    public IEnumerator Enemy_Attack_movement()
    {
        EnemyEffects[0].SetActive(true);
        AM.Play("S");
        yield return new WaitForSecondsRealtime(1.5f);
        EnemyEffects[0].SetActive(false);
        enemy.gameObject.GetComponent<Animator>().SetBool("CanOpen", true);
        yield return new WaitForSecondsRealtime(1.0f);
        IBM.AttackNormalIA();
        yield return new WaitForSecondsRealtime(1.5f);
        enemy.gameObject.GetComponent<Animator>().SetBool("CanOpen", false);
        yield return new WaitForSecondsRealtime(0.5f);
        NextTurn();
    }

    public IEnumerator Enemy_Skill_movement()
    {
        IAisfinish = true;
        GetIndexEffectIa();
        EnemyEffects[indexEffect].SetActive(true);
        AM.Play(enemy.skills[Index].typeAttack.ToString());
        yield return new WaitForSecondsRealtime(1.5f);

        EnemyEffects[indexEffect].SetActive(false);
        enemy.gameObject.GetComponent<Animator>().SetBool("CanOpen", true);
        yield return new WaitForSecondsRealtime(1.0f);

        IBM.AttackSkillIA(Index);
        yield return new WaitForSecondsRealtime(1.5f);

        enemy.gameObject.GetComponent<Animator>().SetBool("CanOpen", false);

        yield return new WaitForSecondsRealtime(0.5f);
        NextTurn();
    }

    public void GetIndexEffectIa()
    {
        switch (enemy.skills[Index].typeAttack)
        {
            case 'S':
                indexEffect = 0;
                break;
            case 'L':
                indexEffect = 1;
                break;
            case 'P':
                indexEffect = 2;
                break;
            case 'F':
                indexEffect = 3;
                break;
            case 'I':
                indexEffect = 4;
                break;
            case 'H':
                indexEffect = 5;
                break;
        }
    }

    public IEnumerator Enemy_make_Miss()
    {
        EnemyEffects[5].SetActive(true);
        yield return new WaitForSecondsRealtime(1.0f);
        EnemyEffects[5].SetActive(false);
    }

    IEnumerator FinishBattle_Effects()
    {
        // Effects
        IsBattleFinish = true; 
        AM.Pause("BattleSong");
        AM.Play("WinBattle");
        // Play Win Song

        yield return new WaitForSecondsRealtime(1f);
        enemy.GetComponent<Animator>().SetBool("Died", true);

        yield return new WaitForSecondsRealtime(3f);

        BattleFinishAnim.SetBool("Dead", true);
        ExGainInBattle();
    }

    public IEnumerator ObjectReciveDamage_Effects(GameObject gameObject)
    {
        // Sound
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSecondsRealtime(0.3f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        yield return new WaitForSecondsRealtime(0.3f);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSecondsRealtime(0.3f);
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
    
    IEnumerator ReturnNavEffect()
    {
        AM.Play("Confirmation");
        NS.Cameras[1].SetActive(false);
        NS.Cameras[0].SetActive(true);
        BattleFinishAnim.SetBool("Dead", false);
        IsBattle = false;
        IsBattleFinish = false;
        yield return new WaitForSecondsRealtime(0.1f);
        NS.ReturnToNav();
    }
    #endregion
}