using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AllClear : MonoBehaviour, IPointerClickHandler
{
    public bool allActive;
    public string currentDate;

    void Start()
    {
        allActive = false;
        currentDate = GameObject.Find("Quests").GetComponent<Quests>().currentDate;
    }

    void Update()
    {
        int completes = 0;
        int counter = 0;

        //날짜가 같은것들 중 다 완료했으면 변해라
        for (int i = 0; i < GameObject.Find("EventManager").GetComponent<QuestsManager>().dates.Count; i++)
        {
            if(GameObject.Find("EventManager").GetComponent<QuestsManager>().currentDate == GameObject.Find("EventManager").GetComponent<QuestsManager>().dates[i])
            {
                if(GameObject.Find("EventManager").GetComponent<QuestsManager>().completes[i] == true)
                {
                    completes += 1;
                }
                counter += 1;
            }
        }

        if (completes == counter && completes != 0)
            TurnBlack();
        else
            TurnGray();
    }

    public void OnPointerClick(PointerEventData data)
    {
        if(allActive == true)
        {
            AllComplete();
        }
        GameObject.Find("Quests").GetComponent<Quests>().SaveData();
    }

    public void AllComplete()
    {
        if (GameObject.Find("EventManager").GetComponent<QuestsManager>().currentCount == 0)
            Debug.Log("아직 아무것도 안만들었는데!");
        else
        {
            Debug.Log("AllComplete!");
            GameObject.Find("Quests").GetComponent<Quests>().completedDates.Add(currentDate);
        }
    }

    public void TurnBlack()
    {
        allActive = true;
        GameObject.Find("AllClear").transform.GetChild(0).GetComponent<Text>().color = Color.black;
    }

    public void TurnGray()
    {
        allActive = false;
        GameObject.Find("AllClear").transform.GetChild(0).GetComponent<Text>().color = Color.gray;
        if (GameObject.Find("Quests").GetComponent<Quests>().completedDates.Contains(currentDate))
        {
            GameObject.Find("Quests").GetComponent<Quests>().completedDates.Remove(currentDate);
        }
    }
}
