using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossBattle : MonoBehaviour
{
    public Player player;
    public int[] index;
    public bool useCard;
    public bool IsNotNormalBoss;
    public bool CanBeOpen;
    public Enemy Boss;
    public NavegationSystem navegationSystem;

    private void Start() 
    {
        navegationSystem = GameObject.FindObjectOfType<NavegationSystem>();
        player = FindObjectOfType<Player>();
        if(player.InTheSaveZone[index[0]] == true)
        {
            if(useCard)
            {
                if(player.HaveKey[index[1]])
                {
                    Destroy(this.gameObject);
                }
            }
            else{ Destroy(this.gameObject); }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        CanBeOpen = true;
    }

    private void Update() 
    {
        if(CanBeOpen)
        {
            navegationSystem.InitBossBattle(Boss, IsNotNormalBoss);
            Destroy(this.gameObject);
        }
    }
}
