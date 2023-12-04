using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public bool CanUp;
    public Camera MainCamera;
    public GameObject Player;
    NavegationSystem NS;

    // Update is called once per frame
    private void Start() 
    {
        NS = FindObjectOfType<NavegationSystem>();
    }

    void Update()
    {
        CheckUpSize();
    }

    public void CheckUpSize()
    {
        if(CanUp)
        {
            MainCamera.orthographicSize = -Player.transform.position.y;
        }
        else
        {
            MainCamera.orthographicSize = 5;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        CanUp = true;
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        CanUp = false;
    }
}