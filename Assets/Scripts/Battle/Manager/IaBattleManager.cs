using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IaBattleManager : MonoBehaviour
{
    #region Vars
    public BattleManager BM;
    #endregion
    #region Init
    // Start is called before the first frame update
    private void Awake() 
    {
       BM = FindObjectOfType<BattleManager>(); 
    }
    // Update is called once per frame
    void Update()
    {
        if(BM.IsBattle && !BM.IsBattleFinish)
        {
            CheckTurnIa();
        }
    }
    #endregion
    #region IA Combat System
    public void DecideTypeAttack_Enemy()
    {
        if(!BM.InPlayerTurn && !BM.IAisfinish)
            Debug.Log("Deciding");
            BM.Index = UnityEngine.Random.Range(0,BM.enemy.skills.Length);
            if(CanMakeAskill_Enemy() && BM.player.Hp >= 10)
            {   
                Debug.Log("Iam Going to make a skill");          
                StartCoroutine(BM.Enemy_Skill_movement());
                BM.IAisfinish = true; 
            }
            else if(!CanMakeAskill_Enemy() || BM.player.Hp <= 10)
            {
                Debug.Log("Iam Going to make a attack");
                StartCoroutine(BM.Enemy_Attack_movement());
                BM.IAisfinish = true;
            }
    }

    public void AttackNormalIA()
    {
        if(BM.enemy.Hp > 0)
        {
            int Dmg = 0;
            int RollAttack = (UnityEngine.Random.Range(0, 50) + BM.enemy.Ag);
            int RollCritic = (UnityEngine.Random.Range(0, 100) + BM.enemy.lu) - BM.player.Ag;
            if(RollAttack > (25 + BM.enemy.Lv))
            {
                Dmg = (BM.enemy.BaseDamage + BM.enemy.St) - BM.player.En;
                if(RollCritic >= 70)
                {
                    Dmg *= 2;
                }
                if(Dmg < 0)
                {
                    Dmg = 0;
                }
                StartCoroutine(BM.ObjectReciveDamage_Effects(BM.playerGO));
                BM.player.Hp -= Dmg;
                BM.BattleText.text = "El  enemigo  ha  hecho  " + Dmg + "  de  dmg.";
            }
            else
            {
                BM.BattleText.text = "El  enemigo  fallo  el  ataque.";
                StartCoroutine(BM.Enemy_make_Miss());
            }
        }
    }

    public void AttackSkillIA(int index)
    {
        if(BM.enemy.Hp > 0)
        {
            int Dmg = 0;
            int Dice = (UnityEngine.Random.Range(0, 50) + BM.enemy.Ag) - BM.player.Ag;
            int CriticDice = (UnityEngine.Random.Range(0, 100) + BM.enemy.lu) - BM.player.Ag;
            if(Dice >= 25)
            {
                Dmg = (BM.enemy.skills[index].Dmg + BM.enemy.Ma) - BM.player.En;
                if(CriticDice > 75)
                {
                    Dmg *= 2;
                }
                if(Dmg < 0)
                {
                    Dmg = 0;
                }
                StartCoroutine(BM.ObjectReciveDamage_Effects(BM.playerGO));
                BM.player.Hp -= Dmg;
                BM.enemy.Sp -= BM.enemy.skills[index].SpRest;
                BM.BattleText.text = "El  enemigo  ha  hecho  " + Dmg + "  de  dmg.";            
            }
            else
            {
                BM.BattleText.text = "El  enemigo  fallo  el  ataque.";            
                StartCoroutine(BM.Enemy_make_Miss());
            }
        }
    }
    
    bool CanMakeAskill_Enemy()
    {
        int Result = BM.enemy.Sp - BM.enemy.skills[BM.Index].SpRest;
        if(Result < 0)
        {
            BM.AM.Play("Confirmation");
            return false;
        }
        else if(Result >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CheckTurnIa()
    {
        if(!BM.InPlayerTurn && !BM.IAisfinish && BM.enemy.Hp > 0)
        {
            DecideTypeAttack_Enemy();
            BM.IAisfinish = true;
        }
        if(BM.InPlayerTurn)
        {
            BM.IAisfinish = false;
        }
    }
    #endregion
}
