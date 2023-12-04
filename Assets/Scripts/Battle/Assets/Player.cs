using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : MonoBehaviour
{
    // Battle
    public int Lv = 0;
    public int Hp = 25;
    public int ExLeftToNextlvl = 20; // This * LV
    public int Ex = 0;
    public int HpMax = 25;
    public int Sp = 15;
    public int SpMax = 15;
    public int BaseDmg = 5;
    // Stats
    public int St = 3; // Strength
    public int En = 4; // Endurance
    public int Ma = 3; // Magic
    public int Ag = 4; // Agility
    public int lu = 2; // Luck
    // Skills
    public Skills[] skills;
    // Items
    public int Medicide = 0; 
    public bool[] HaveKey;
    // Extras
    public int IndexStorys;
    public bool[] InTheSaveZone;

    // Save And Load
    public int Scene;

    public void SavePlayerData(string NameFile)
    {
        TraslatePlayerData.SavePlayerData(this, NameFile);
    }

    public void LoadPlayerData(bool UsePosition, bool LoadArrays, string NameFile)
    {
        PlayerData data = TraslatePlayerData.LoadPlayerData(this, NameFile);

        if(data != null)
        {
            // Player Sett
            Lv = data.Lv;
            Hp = data.Hp;
            
            ExLeftToNextlvl = data.ExLeftToNextlvl;
            Ex = data.Ex;
            HpMax = data.HpMax;
            Sp = data.Sp;
            SpMax = data.SpMax;
            BaseDmg = data.BaseDmg;

            // Player Stats
            St = data.St;
            En = data.En;
            Ma = data.Ma;
            Ag = data.Ag;
            lu = data.lu;

            // Items
            Medicide = data.Medicide;

            // Extras
            IndexStorys = data.IndexStorys;

            // Position
            if(UsePosition)
            {
                this.gameObject.transform.position = new Vector3(data.x, data.y, 0);
                GameObject MoveToHere = GameObject.Find("moveTohere");
                MoveToHere.transform.position = new Vector3(data.x, data.y, 0);
            }

            // Save And Load
            Scene = data.Scene;
            
            if(LoadArrays)
            {
                // Items
                HaveKey[0] = data.HaveKey[0];
                HaveKey[1] = data.HaveKey[1];
                HaveKey[2] = data.HaveKey[2]; 

                // Extras
                InTheSaveZone[0] = data.InTheSaveZone[0];
                InTheSaveZone[1] = data.InTheSaveZone[1];
                InTheSaveZone[2] = data.InTheSaveZone[2]; 
            }
        }
    }
}
