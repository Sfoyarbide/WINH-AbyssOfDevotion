using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int Lv;
    public int Hp;
    public int ExLeftToNextlvl;
    public int Ex;
    public int HpMax;
    public int Sp;
    public int SpMax;
    public int BaseDmg;

    public int St; 
    public int En; 
    public int Ma; 
    public int Ag; 
    public int lu; 
    // Skills
    public Skills[] skills;
    // Items
    public int Medicide; 
    public bool[] HaveKey = new bool[3];
    // Extras
    public int IndexStorys;
    public bool[] InTheSaveZone = new bool[3];
    public int Scene;

    // Position
    public float x,y; 
    public PlayerData(Player player)
    {
        // Player Sett
        Lv = player.Lv;
        Hp = player.Hp;
        ExLeftToNextlvl = player.ExLeftToNextlvl;
        Ex = player.Ex;
        HpMax = player.HpMax;
        Sp = player.Sp;
        SpMax = player.SpMax;
        BaseDmg = player.BaseDmg;

        // Player Stats
        St = player.St;
        En = player.En;
        Ma = player.Ma;
        Ag = player.Ag;
        lu = player.lu;

        // Items
        Medicide = player.Medicide;
        HaveKey[0] = player.HaveKey[0];
        HaveKey[1] = player.HaveKey[1];
        HaveKey[2] = player.HaveKey[2];

        // Extras
        IndexStorys = player.IndexStorys;
        InTheSaveZone[0] = player.InTheSaveZone[0];
        InTheSaveZone[1] = player.InTheSaveZone[1];
        InTheSaveZone[2] = player.InTheSaveZone[2]; 

        // Save And Load
        Scene = player.Scene;

        // Pos
        x = player.gameObject.transform.position.x;
        y = player.gameObject.transform.position.y;
    }
}