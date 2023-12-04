using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Battle
    public int Lv = 1;
    public int Hp = 30;
    public int HpMax = 30;
    public int Sp = 20;
    public int SpMax = 20;
    public int BaseDamage = 4;
    public string Name = "";
    public char Weakness; // Healing, Strike, Slash, Pierce, Ice, Fire;
    // Stats
    public int St = 4; // Strength
    public int En = 5; // Endurance
    public int Ma = 2; // Magic
    public int Ag = 5; // Agility
    public int lu = 1; // Luck
    // Skills
    public Skills[] skills;
}
