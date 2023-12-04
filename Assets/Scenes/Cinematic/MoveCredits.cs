using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveCredits : MonoBehaviour
{
    public RectTransform RectTransform;
    public float speed = 0.3f;
    public bool CanMove;
    public Vector3 ToWhere;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        canMove();
    }

    void canMove()
    {
        if(CanMove)
        {
            RectTransform.position +=  new Vector3(0,speed * Time.deltaTime);
        }
        if(RectTransform.localPosition.y >= ToWhere.y)
        {
            CanMove = false;
            StartCoroutine(WaitToReturn());
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(4f);
        CanMove = true;
    }

    IEnumerator WaitToReturn()
    {
        yield return new WaitForSecondsRealtime(10f);
        SceneManager.LoadScene("Menu");
    }
}
