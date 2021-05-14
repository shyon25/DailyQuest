using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchManager : MonoBehaviour
{
    Touch theTouch;
    Vector2 touchStart, touchEnd, trackingPoint;

    int blockPerPage = 7;
    int topLimit = 100;
    int bottomLimit = 520;
    int questCount;

    float currentFirstY;
    float currentLastY;

    private void Start()
    {
        questCount = GameObject.Find("EventManager").GetComponent<QuestsManager>().currentCount;
        currentFirstY = GameObject.Find("Texts").transform.GetChild(0).GetComponent<RectTransform>().offsetMax.y;
        currentLastY = GameObject.Find("Texts").transform.GetChild(questCount - 1).GetComponent<RectTransform>().offsetMin.y;
    }
    private void Update()
    {
        DetectingSwipe();
    }

    public void DetectingSwipe()
    {
        if(Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Began)
            {
                touchStart = theTouch.position;
                trackingPoint = touchStart;
            }
            else if (theTouch.phase == TouchPhase.Moved || theTouch.phase == TouchPhase.Stationary)
            {
                touchEnd = theTouch.position;
                Moving();
            }
            else
            {
                touchStart = Vector2.zero;
                touchEnd = Vector2.zero;
            }
        }
        else
        {
            if (questCount <= blockPerPage && GameObject.Find("Texts").transform.GetChild(0).GetComponent<RectTransform>().offsetMax.y != -topLimit)
            {
                ReturnTop();
            }
            else if (questCount > blockPerPage)
            {
                if (GameObject.Find("Texts").transform.GetChild(0).GetComponent<RectTransform>().offsetMax.y < -topLimit)
                    ReturnTop();
                else if(GameObject.Find("Texts").transform.GetChild(questCount - 1).GetComponent<RectTransform>().offsetMin.y > bottomLimit)
                    ReturnBottom();
            }
        }
    }
    public void Moving()
    {
        float speed = 0;

        speed = (theTouch.position.y - trackingPoint.y) / 5;

        trackingPoint.y += speed;

        if (speed < 1 && speed > -1)
            speed = 0;
        
        RectTransform position = this.GetComponent<RectTransform>();
        
        position.SetTop(-position.offsetMax.y - speed);
        position.SetBottom(position.offsetMin.y + speed);
        
    }
    public void ReturnTop()
    {
        float speed = 0;

        speed = (-topLimit - currentFirstY) / 5;

        RectTransform position = this.gameObject.GetComponent<RectTransform>();

        position.SetTop(-position.offsetMax.y - speed);
        position.SetBottom(position.offsetMin.y + speed);

        currentFirstY = GameObject.Find("Texts").transform.GetChild(0).GetComponent<RectTransform>().offsetMax.y;
    }
    public void ReturnBottom()
    {
        float speed = 0;

        currentLastY = GameObject.Find("Texts").transform.GetChild(questCount - 1).GetComponent<RectTransform>().offsetMin.y;

        speed = (bottomLimit - currentLastY) / 5;

        RectTransform position = this.gameObject.GetComponent<RectTransform>();

        position.SetTop(-position.offsetMax.y - speed);
        position.SetBottom(position.offsetMin.y + speed);

    }
}
