using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public PlayerNavagatorSystem PNS;
    public GameObject[] buttonsMobile;

    private void Start() 
    {
        PNS = FindObjectOfType<PlayerNavagatorSystem>();
    }

    public void GoToPositiveX()
    {
        PNS.index = 2;
        PNS.x = 1;
    }

    public void GoToPositiveY()
    {
        PNS.index = 0;
        PNS.y = 1;
    }

    public void GoToNegativeX()
    {
        PNS.index = 3;
        PNS.x = -1;
    }

    public void GoToNegativeY()
    {
        PNS.index = 1;
        PNS.y = -1;
    }

    public void TurnOffButtons()
    {
        for (int x = 0; x < buttonsMobile.Length;)
        {
            if(x == PNS.index)
            {
                if(x == 3)
                {
                    break;
                }
                else
                {
                    x++;
                }
            }
            else
            {
                buttonsMobile[x].gameObject.SetActive(false);
                x++;
            }
        }
    }
}