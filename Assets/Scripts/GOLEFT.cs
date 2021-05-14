using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GOLEFT : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData data)
    {
        GoLeft();
    }

    public void GoLeft()
    {
        DestroyCalendar();

        string now = GameObject.Find("Date").GetComponent<Text>().text;
        int year = int.Parse(now.Substring(0, 4));
        int month = int.Parse(now.Substring(5, 2));
        if(month == 1)
        {
            year -= 1;
            month = 12;
        }
        else
        {
            month -= 1;
        }
        if(month >= 10)
            GameObject.Find("Date").GetComponent<Text>().text = year.ToString() + "-" + month.ToString();
        else
            GameObject.Find("Date").GetComponent<Text>().text = year.ToString() + "-0" + month.ToString();
        GameObject.Find("Quests").GetComponent<Quests>().currentDate = GameObject.Find("Date").GetComponent<Text>().text;
        GameObject.Find("CalendarManager").GetComponent<CalendarManager>().InitDates();
    }
    public void DestroyCalendar()
    {
        for (int i = 0; i < 42; i++)
        {
            Destroy(GameObject.Find("Calendar").transform.GetChild(i).gameObject);
        }
    }
}
