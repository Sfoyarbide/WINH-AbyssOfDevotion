using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Skills", menuName = "Battle/Skills", order = 0)]
public class Skills : ScriptableObject 
{
    public string Name = "";
    public int Dmg = 0;
    public int SpRest = 0;
    public char typeAttack; // Healing, Strike, Slash, Pierce, Ice, Fire;
}