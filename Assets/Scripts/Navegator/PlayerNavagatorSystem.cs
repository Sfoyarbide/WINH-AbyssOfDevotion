using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNavagatorSystem : MonoBehaviour
{
    public int index;
    public float x = 0, y = 0;
    public Animator PlayerAnim;
    public NavegationSystem navegationSystem;
    InputTester IT;
    public UiStatus uiStatus;
    public BattleManager BM;
    public float Speed = 3f;
    public Transform MoveToHere;
    public LayerMask collidersStop, collidersEnemy;
    bool ShowingStatus;
    public bool CanMove = true;
    public bool IsMoving = false;
    
    private void Start() 
    {
        BM = FindObjectOfType<BattleManager>();
        uiStatus = GameObject.Find("UI").GetComponent<UiStatus>();
        IT = FindObjectOfType<InputTester>();
    }

    // Update is called once per frame
    void Update()
    {
        // PC
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
        MoveSystem();
        UpdateAnim();
        if(Input.GetKeyDown(KeyCode.P) && uiStatus.gameObject.transform.GetChild(7).gameObject.activeInHierarchy) // PC
        {
            OpenStatus();
        }
    }

    void UpdateAnim()
    {
        PlayerAnim.SetFloat("PosX", x);
        PlayerAnim.SetFloat("PosY", y);
        if(!CanMove)
        {
            PlayerAnim.SetBool("ismove",false);
        }
    }

    public void OpenStatus()
    {
        if(!BM.IsBattle)
        {
            ShowingStatus = !ShowingStatus;
            CanMove = !ShowingStatus;
            uiStatus.UpdateStats();
            uiStatus.transform.GetChild(4).gameObject.SetActive(ShowingStatus);
            if(ShowingStatus)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void MoveSystem()
    {
        if(!BM.IsBattle && CanMove)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, MoveToHere.position, Speed * Time.deltaTime);

            if(Vector3.Distance(this.transform.position, MoveToHere.position) <= 0.02f)
            {
                PlayerAnim.SetBool("ismove", false);
                if(Mathf.Abs(x) == 1f)
                {
                    if(!Physics2D.OverlapCircle(MoveToHere.position + new Vector3(x, 0, 0), .2f, collidersStop))
                    {
                        PlayerAnim.SetBool("ismove", true);
                        MoveToHere.position += new Vector3(x, 0, 0);
                        BM.AM.Play("Walking");

                        if(navegationSystem.CanEnterBattle && !IT.TEMP)
                        {
                            StartCoroutine(CheckIsBattle());  
                        }
                    }
                }
                else if(Mathf.Abs(y) == 1f)
                {
                    if(!Physics2D.OverlapCircle(MoveToHere.position + new Vector3(0, y, 0), .2f, collidersStop))
                    {
                        PlayerAnim.SetBool("ismove", true);
                        MoveToHere.position += new Vector3(0, y, 0);
                        BM.AM.Play("Walking");

                        if(navegationSystem.CanEnterBattle && !IT.TEMP)
                        {
                            StartCoroutine(CheckIsBattle());         
                        }          
                    }
                }
            }
        }
    }

    IEnumerator CheckIsBattle()
    {
        yield return new WaitForSeconds(0.2f);
        navegationSystem.InitBattle();
    }
}