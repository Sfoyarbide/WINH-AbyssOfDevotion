using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HandleButtons : MonoBehaviour, IPointerUpHandler
{
    public PlayerNavagatorSystem PNS;
    public MobileInput MI;

    private void Start() 
    {
        PNS = FindObjectOfType<PlayerNavagatorSystem>();
        MI = FindObjectOfType<MobileInput>();
    }

    bool isPress;

    public void IsHold()
    {
        isPress = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPress = false;
    }

    private void Update() 
    {
        if(!isPress)
        {
            TurnOn();
        }
    }

    void TurnOn()
    {
        for(int x = 0; x < MI.buttonsMobile.Length; x++)
        {
            MI.buttonsMobile[x].gameObject.SetActive(true);
            PNS.x = 0;
            PNS.y = 0;
        }
    }
}
