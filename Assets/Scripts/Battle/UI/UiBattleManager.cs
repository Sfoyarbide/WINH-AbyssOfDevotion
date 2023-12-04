using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiBattleManager : MonoBehaviour
{
    public Animator BattleStatus;
    public BattleManager BM;
    public GameObject AttackButton;
    public GameObject SkillButton;
    public GameObject medicineButton;
    public GameObject SlotSkillManager;
    public GameObject[] SlotsSkills;
    public GameObject[] PlayerEffect;
    public GameObject playerGO;
    public Slider[] sliders;
    public Text[] namesAndLvl;
    public int index;
    public int indexEffect;
    public bool ShowSkill;
    public Skills medicineAux;
    public Text MedicineBattleStatus;

    private void Start() 
    {
        BM = GameObject.Find("Battle").GetComponent<BattleManager>();
    }

    // Update is called once per frame
    private void Update() 
    {
        UpdateUI();
        UpdateText_SkillsSlots();
        UpdateColors_SlotSkills();
        inputSkillSlots();
    }

    // Ui manager
    public void UpdateUI()
    {
        if(BM.IsBattle)
        {
            namesAndLvl[0].text = "LvL: " + BM.player.Lv.ToString(); // lvl Player
            namesAndLvl[1].text = "Hp: " + BM.player.Hp.ToString(); // Hp Player
            namesAndLvl[2].text = "" + BM.enemy.name; // Enemy Name
            namesAndLvl[3].text = "LvL: " + BM.enemy.Lv.ToString(); // lvl Enemy
            namesAndLvl[4].text = "Hp: " + BM.enemy.Hp.ToString(); // Hp Enemy
            namesAndLvl[5].text = "Sp: " + BM.player.Sp.ToString(); // Sp Player
            namesAndLvl[6].text = "Sp: " + BM.enemy.Sp.ToString(); // Sp Enemy

            // Hp
            sliders[0].maxValue = BM.player.HpMax;
            sliders[0].value = BM.player.Hp;
            sliders[1].maxValue = BM.enemy.HpMax;
            sliders[1].value = BM.enemy.Hp;

            // Sp
            sliders[2].maxValue = BM.player.SpMax;
            sliders[2].value = BM.player.Sp;
            sliders[3].maxValue = BM.enemy.SpMax;
            sliders[3].value = BM.enemy.Sp;      

            // Medicine
            MedicineBattleStatus.text = "Use  Medicine                                     " + BM.player.Medicide.ToString();
        }
    }

    // Attack
    public void Attack_Player_INIT()
    {
        if(!BM.InAction && BM.InPlayerTurn && !BM.IAisfinish)
        {
            AttackButton.SetActive(false);
            SkillButton.SetActive(false);
            medicineButton.SetActive(false);
            BattleStatus.SetBool("Go", false);
            BattleStatus.SetBool("IsBattleFinish", false);
            StartCoroutine(Player_Attack_Movement());
        }
    }

    public void GetIndexEffect(char Type)
    {
        switch (Type)
        {
            case 'S':
                indexEffect = 0;
                break;
            case 'L':
                indexEffect = 1;
                break;
            case 'A':
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
            case 'M':
                indexEffect = 8;
                break;
        }
    }

    public IEnumerator Player_Attack_Movement()
    {
        BM.InAction = true;
        PlayerEffect[1].SetActive(true);
        BM.AM.Play("L");

        yield return new WaitForSecondsRealtime(1.5f);

        PlayerEffect[1].SetActive(false);
        playerGO.GetComponent<Animator>().SetBool("CanOpen", true);
        yield return new WaitForSecondsRealtime(1.0f);

        Attack_Player();
        yield return new WaitForSecondsRealtime(1.5f);

        playerGO.GetComponent<Animator>().SetBool("CanOpen", false);
        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log("Before next turn player");
        BM.NextTurn();
    }

    public IEnumerator Player_Skill_Movement()
    {
        GetIndexEffect(BM.player.skills[index].typeAttack);
        ActiveSkill();
        PlayerEffect[indexEffect].SetActive(true);
        BM.AM.Play(BM.player.skills[index].typeAttack.ToString());
        yield return new WaitForSecondsRealtime(1.5f);

        PlayerEffect[indexEffect].SetActive(false);
        playerGO.GetComponent<Animator>().SetBool("CanOpen", true);
        yield return new WaitForSecondsRealtime(1.0f);

        RollSkill();

        yield return new WaitForSecondsRealtime(1.5f);
        playerGO.GetComponent<Animator>().SetBool("CanOpen", false);

        yield return new WaitForSecondsRealtime(0.5f);
        BM.NextTurn();
    }

    public IEnumerator Player_Medicine_Movement()
    {
        turnOffButtons();
        indexEffect = 8;
        PlayerEffect[indexEffect].SetActive(true);
        BM.AM.Play("H");
        yield return new WaitForSecondsRealtime(1.5f);

        PlayerEffect[indexEffect].SetActive(false);
        playerGO.GetComponent<Animator>().SetBool("CanOpen", true);
        yield return new WaitForSecondsRealtime(1.0f);

        Healing_Skill(false);

        yield return new WaitForSecondsRealtime(1.5f);
        playerGO.GetComponent<Animator>().SetBool("CanOpen", false);

        yield return new WaitForSecondsRealtime(0.5f);
        BM.NextTurn();
    }

    public IEnumerator Player_make_Weak()
    {
        PlayerEffect[7].SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        PlayerEffect[7].SetActive(false);
    }

    public IEnumerator Player_make_Miss()
    {
        PlayerEffect[6].SetActive(true);
        yield return new WaitForSecondsRealtime(1.0f);
        PlayerEffect[6].SetActive(false);
    }

    public void Attack_Player()
    {
        if(BM.InPlayerTurn)
        {
            int Dmg = 0;
            int RollAttack = (UnityEngine.Random.Range(0, 100) + BM.player.Ag);
            if(RollAttack >= (25 + BM.player.Lv))
            {
                Dmg = (BM.player.BaseDmg + BM.player.St) - BM.enemy.En;
                if(BM.enemy.Weakness == 'L') // sLash
                {
                    Dmg += (10) + (2 * BM.player.Ma);
                    StartCoroutine(Player_make_Weak());  
                }
                if(Dmg < 0)
                {
                    Dmg = 0;
                }
                
                StartCoroutine(BM.ObjectReciveDamage_Effects(BM.enemy.gameObject));
                BM.enemy.Hp -= Dmg;
                BM.BattleText.text = "Atenea  ha  hecho  " + Dmg + "  de  dmg.";
            }
            else
            {
                // Miss
                BM.BattleText.text = "Atenea  fallo  el  ataque.";
                StartCoroutine(Player_make_Miss());
            }
        }
    }

    // Skills
    public void RollSkill()
    {   
        if(CanMakeAskill() && BM.InPlayerTurn)
        {
            switch(BM.player.skills[index].typeAttack)
            {
                case 'H':
                    Healing_Skill(true);
                    break;
                default:
                    AttackStrike_Skill();
                    break;
            }
        }
    }

    void AttackStrike_Skill()
    {
        int Dice = (UnityEngine.Random.Range(0, 100) + BM.player.Ag);
        if(Dice >= (25 + BM.player.Lv))
        {
            int Dmg = BM.player.skills[index].Dmg + BM.player.Ma - BM.enemy.En;

            if(BM.player.skills[index].typeAttack == BM.enemy.Weakness)
            {
                Dmg += (10) + (2 * BM.player.Ma);
                StartCoroutine(Player_make_Weak());
                BM.BattleText.text = "Atenea  ha  sacado  la  debilidad  del  enemigo!,  ha  hecho  " + Dmg + "  de  dmg";
            }
            if(Dmg < 0)
            {
                Dmg = 0;
            }            
            else
            {
                BM.BattleText.text = "Atenea  ha  hecho  " + Dmg + "  de  dmg.";
            }
            
            StartCoroutine(BM.ObjectReciveDamage_Effects(BM.enemy.gameObject));
            BM.enemy.Hp -= Dmg;
        }
        else
        {
            BM.BattleText.text = "Atenea  fallo  el  ataque.";
            StartCoroutine(Player_make_Miss());   
        }

        BM.player.Sp -= BM.player.skills[index].SpRest;
    }

    void Healing_Skill(bool isSkill)
    {
        if(isSkill)
        {
            BM.player.Hp += BM.player.skills[index].Dmg;
            if(BM.player.Hp > BM.player.HpMax)
            {
                BM.player.Hp = BM.player.HpMax;
                BM.BattleText.text = "Atenea  se  curo  su  vida  por  completo.";
            }
            else
            {
                BM.BattleText.text = "Atenea  se  ha  curado " + BM.player.skills[index].Dmg.ToString() + "  de  vida";
            }
            BM.player.Sp -= BM.player.skills[index].SpRest;
        }
        else
        {
            BM.player.Hp += medicineAux.Dmg;
            if(BM.player.Hp > BM.player.HpMax)
            {
                BM.player.Hp = BM.player.HpMax;
                BM.BattleText.text = "Atenea  se  curo  su  vida  por  completo.";
            }
            else
            {
                BM.BattleText.text = "Atenea  se  ha  curado " + medicineAux.Dmg.ToString() + "  de  vida";
            }
            BM.player.Sp -= medicineAux.SpRest;
        }
    }

    bool CanMakeAskill()
    {
        int Result = BM.player.Sp - BM.player.skills[index].SpRest;
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
   

    // UI Skill
    public void MedicineButton()
    {
        if(BM.player.Medicide > 0 && !BM.InAction)
        {
            StartCoroutine(Player_Medicine_Movement());
            BM.player.Medicide--;
            BM.InAction = true;
        }
        else if(BM.player.Medicide <= 0)
        {
            BM.AM.Play("Confirmation");
            BM.BattleText.text = "No hay mas medicinas!";
        }
    }

    void ShowSkillsSlot()
    {
        SlotSkillManager.SetActive(ShowSkill);
    }
    void UpdateText_SkillsSlots()
    {
        if(ShowSkill)
        {
            for(int x = 0; x < SlotsSkills.Length; x++)
            {
                SlotsSkills[x].transform.GetChild(0).GetComponent<Text>().text = BM.player.skills[x].name;
            }
        }
    }
    void inputSkillSlots()
    {
        if(ShowSkill && !BM.InAction && BM.InPlayerTurn && !BM.IAisfinish)
        {
            BattleStatus.SetBool("Go",true);
            if(Input.GetKeyDown(KeyCode.E) && index != 5)
            {
                index++;
                TurnOffColors();
            }
            if(Input.GetKeyDown(KeyCode.Q) && index != 0)
            {
                index--;
                TurnOffColors();
            }
            if(Input.GetKeyDown(KeyCode.X) && !BM.IsBattleFinish)
            {
                if(CanMakeAskill())
                {
                    AttackButton.SetActive(false);
                    SkillButton.SetActive(false);
                    BM.InAction = true;
                    StartCoroutine(Player_Skill_Movement());
                    BattleStatus.SetBool("Go",false);
                }
            }
        }
    }
    void UpdateColors_SlotSkills()
    {   
        if(ShowSkill)
        {
            AttackButton.gameObject.SetActive(false);
            medicineButton.SetActive(false);
            SlotsSkills[index].GetComponent<Image>().color = Color.yellow;
            if(CanMakeAskill())
            {
                BM.BattleText.text = "Esta  habilidad  requiere  " + BM.player.skills[index].SpRest + "  de  Sp.";
            }
            else
            {
                BM.BattleText.text = "No  Hay  Suficiente  SP!";
            }
        }
        else if(!ShowSkill && BM.InPlayerTurn && !BM.InAction)
        {
            AttackButton.gameObject.SetActive(true);
            medicineButton.SetActive(true);
        }
    }
    void TurnOffColors()
    {
        for(int y = 0; y < SlotsSkills.Length; y++)
        {
            SlotsSkills[y].GetComponent<Image>().color = new Color(0,0,0,0);
        }
    }
    public void ActiveSkill()
    {
        if(BM.InPlayerTurn && BM.IsBattle && !BM.IsBattleFinish)
        {
            ShowSkill = !ShowSkill;
            ShowSkillsSlot();
            BM.AM.Play("Confirmation");
            if(!ShowSkill)
            {
                BM.BattleText.text = "Esperando a Atenea!.";
                BattleStatus.SetBool("Go", false);
            }
        }
    }

    void turnOffButtons()
    {
        AttackButton.SetActive(false);
        medicineButton.SetActive(false);
        SkillButton.SetActive(false);
    }

    public void Up_button()
    {
        if(ShowSkill && !BM.InAction && BM.InPlayerTurn && !BM.IAisfinish)
        {
            if(index != 0)
            {
                index--;
                TurnOffColors();
            }
        }
    }

    public void Down_button()
    {
        if(ShowSkill && !BM.InAction && BM.InPlayerTurn && !BM.IAisfinish)
        {
            if(index != 5)
            {
                index++;
                TurnOffColors();
            }
        }
    }

    public void AccionButton()
    {
        if(ShowSkill && !BM.InAction && BM.InPlayerTurn && !BM.IAisfinish)
        {
            if(!BM.IsBattleFinish)
            {
                if(CanMakeAskill())
                {
                    AttackButton.SetActive(false);
                    SkillButton.SetActive(false);
                    BM.InAction = true;
                    StartCoroutine(Player_Skill_Movement());
                    BattleStatus.SetBool("Go", false);
                    BattleStatus.SetBool("IsBattleFinish", false);
                }
            }
        }       
    }
}
