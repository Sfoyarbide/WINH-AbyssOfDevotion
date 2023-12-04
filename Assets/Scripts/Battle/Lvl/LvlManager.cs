using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlManager : MonoBehaviour
{
    Player player;
    public Skills[] AllSkills;

    // Start is called before the first frame update

    public void CheckLvL(Player Recive)
    {
        player = Recive;
        #region "Tier2"
        if(player.Lv >= 2)
        {
            player.skills[0] = AllSkills[0]; // Fajroin   
        }
        if(player.Lv >= 4)
        {
            player.skills[1] = AllSkills[1]; // Glaciesin 
        }
        if(player.Lv >= 6)
        {
            player.skills[4] = AllSkills[2]; // Arrow 
        }
        if(player.Lv >= 8)
        {
            player.skills[3] = AllSkills[3]; // Slash
        }
        if(player.Lv >= 10)
        {
            player.skills[2] = AllSkills[4]; // Strike
        }
        if(player.Lv >= 12)
        {
            player.skills[5] = AllSkills[5]; // Healing
        }
        #endregion
        #region "Tier3"
        if(player.Lv >= 14)
        {
            player.skills[2] = AllSkills[6]; // Strike   
        }
        if(player.Lv >= 16)
        {
            player.skills[1] = AllSkills[7]; // Ice
        }
        if(player.Lv >= 18)
        {
            player.skills[0] = AllSkills[8]; // Fire
        }
        if(player.Lv >= 20)
        {
            player.skills[4] = AllSkills[9]; // Arrow
        }
        if(player.Lv >= 22)
        {
            player.skills[5] = AllSkills[10]; // Healing
        }
        if(player.Lv >= 24)
        {
            player.skills[3] = AllSkills[11]; // Slash
        }
        #endregion
        #region "Extra"
        if(player.Lv >= 30)
        {
            player.skills[2] = AllSkills[12]; // Strike, the extra...
        }
        #endregion
    }
}
