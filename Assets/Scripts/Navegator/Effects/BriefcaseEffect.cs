using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefcaseEffect : MonoBehaviour
{
    public PlayerNavagatorSystem PNS;
    public GameObject[] Effects;

    private void Start() 
    {
        PNS = FindObjectOfType<PlayerNavagatorSystem>();
        Effects[0] = this.transform.GetChild(0).gameObject;
        Effects[1] = this.transform.GetChild(1).gameObject;
        Effects[2] = this.transform.GetChild(2).gameObject;
        Effects[3] = this.transform.GetChild(3).gameObject;
    }

    public IEnumerator ShowPick(int index, GameObject briefcase)
    {
        PNS.CanMove = false;
        Effects[index].SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        Effects[index].SetActive(false);
        PNS.CanMove = true;
        Destroy(briefcase);
    }
}
