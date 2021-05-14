using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public static class RectTransformExtensions
{
    public static void SetLeft(this RectTransform rt, float left)
    {
        rt.offsetMin = new Vector2(left, rt.offsetMin.y);
    }

    public static void SetRight(this RectTransform rt, float right)
    {
        rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
    }

    public static void SetTop(this RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }

    public static void SetBottom(this RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }
}

public class QuestsManager : MonoBehaviour
{
    public string currentDate;
    public List<string> quests;
    public List<string> dates;
    public List<bool> completes;
    GameObject AddField;
    InputField input_Field;
    public int AddFieldIndex;

    InputField FromField;
    InputField ToField;

    GameObject CompleteButton;

    public GameObject TextBlock;

    public int currentCount;
    
    GameObject currentBlock;

    // Start is called before the first frame update
    void Start()
    {
        AddField = GameObject.Find("AddField");
        input_Field = AddField.transform.GetChild(2).GetComponent<InputField>();
        FromField = AddField.transform.GetChild(3).GetComponent<InputField>();
        ToField = AddField.transform.GetChild(4).GetComponent<InputField>();
        AddField.SetActive(false);

        CompleteButton = GameObject.Find("AllClear2");
        CompleteButton.SetActive(false);
        
        quests = new List<string>(GameObject.Find("Quests").GetComponent<Quests>().quests);
        dates = new List<string>(GameObject.Find("Quests").GetComponent<Quests>().date);
        completes = new List<bool>(GameObject.Find("Quests").GetComponent<Quests>().completes);
        currentDate = GameObject.Find("Quests").GetComponent<Quests>().currentDate;

        currentCount = 0;

        WriteToday();
    }

    // Update is called once per frame
    void Update()
    {
        int currentQuests = 0;

        for(int i = 0; i < quests.Count; i++)
        {
            if (dates[i] == currentDate)
                currentQuests += 1;
        }

        if (currentCount != currentQuests)
        {
            Drawit();
        }

        if (GameObject.Find("Quests").GetComponent<Quests>().completedDates.Contains(currentDate))
            CompleteButton.SetActive(true);
        else
            CompleteButton.SetActive(false);
    }

    public void CallAddField()
    {
        FromField.Select();
        FromField.text = currentDate;// 시작날짜 입력
        ToField.Select();
        ToField.text = currentDate;// 끝날짜 입력
        AddField.SetActive(true);
    }
    public void CancelAddField()
    {
        input_Field.Select();
        input_Field.text = ""; //텍스트 초기화
        AddField.SetActive(false);
    }

    public void MakeQuest()
    {
        string from = FromField.text;
        string to = ToField.text;

        if (from == "")
            from = currentDate;
        if (to == "")
            to = currentDate;

        Debug.Log(from + ", " + to);

        for(string day = from; day != NextDay(to); day = NextDay(day))
        {
            quests.Add(input_Field.text);
            dates.Add(day);
            completes.Add(false);
        }
        
        Saveit();
        CancelAddField();
        GameObject.Find("Quests").GetComponent<Quests>().SaveData();
    }

    public string NextDay(string day)
    {
        int year = int.Parse(day.Substring(0, 4));
        int month = int.Parse(day.Substring(5, 2));
        int date = int.Parse(day.Substring(8, 2));
        int[] daysOfMonth = new int[12] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        if (isLeap(year))
            daysOfMonth[1] = 29;

        if(date != daysOfMonth[month - 1])
            return stringMaker(year, month, date + 1);//평일임
        else
        {
            if (month != 12)
                return stringMaker(year, month + 1, 1);// 월말임
            else
                return stringMaker(year + 1, 1, 1);//연말임
        }
    }
    public bool isLeap(int year)
    {
        bool four = false;
        bool hundred = false;
        bool four_hundred = false;

        if (year % 4 == 0)
            four = true;
        if (year % 100 == 0)
            hundred = true;
        if (year % 400 == 0)
            four_hundred = true;

        return (four && !hundred) || four_hundred;
    }
    public string stringMaker(int year, int month, int date)
    {
        string day, stringYear, stringMonth, stringDate;

        stringYear = year.ToString();
        if (month >= 10)
            stringMonth = month.ToString();
        else
            stringMonth = "0" + month.ToString();
        if (date >= 10)
            stringDate = date.ToString();
        else
            stringDate = "0" + date.ToString();

        day = stringYear + "-" + stringMonth + "-" + stringDate;

        return day;
    }

    public void DestroyQuest()
    {

    }

    public void Saveit()
    {
        GameObject.Find("Quests").GetComponent<Quests>().quests = new List<string>(quests);
        GameObject.Find("Quests").GetComponent<Quests>().date = new List<string>(dates);
        GameObject.Find("Quests").GetComponent<Quests>().completes = new List<bool>(completes);
    }

    public void Drawit() // 앞의 그림을 모두 부수고 지정한 날짜와 같은 퀘스트들을 모두 불러와 그린다.
    {
        List<string> drawingQuest = new List<string>();
        List<string> drawingDate = new List<string>();
        List<bool> drawingComplete = new List<bool>();
        List<int> drawingNumber = new List<int>();

        InitiateDraw();

        for(int d = 0; d < dates.Count; d++)
        {
            if (currentDate == dates[d])
            {
                drawingDate.Add(dates[d]);
                drawingQuest.Add(quests[d]);
                drawingComplete.Add(completes[d]);
                drawingNumber.Add(d);
            }
        }

        for (int d = 0; d < drawingDate.Count; d++)
        {
            currentBlock = GameObject.Instantiate(TextBlock);
            currentBlock.name = d.ToString();
            currentBlock.transform.GetChild(0).name = drawingNumber[d].ToString();
            currentBlock.transform.SetParent(GameObject.Find("Texts").transform);
            currentBlock.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            currentBlock.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            SetFour(currentBlock.GetComponent<RectTransform>(), 100, 100, 100 + 200 * d, 1720 - 200 * d);//블럭 사이즈
            currentBlock.transform.GetChild(0).GetComponent<Text>().text = drawingQuest[d];
            CheckComplete(d, drawingComplete);
        }
        currentCount = drawingDate.Count;
    }

    public void InitiateDraw()
    {
        int children = GameObject.Find("Texts").transform.childCount;
        for(int i = 0; i<children; i++)
        {
            Destroy(GameObject.Find("Texts").transform.GetChild(i).gameObject);
        }
    }

    public void SetFour(RectTransform rt, float left, float right, float top, float bottom)
    {
        rt.SetLeft(left);
        rt.SetRight(right);
        rt.SetTop(top);
        rt.SetBottom(bottom);
    }

    public void CheckComplete(int d, List<bool> drawingComplete)
    {
        currentBlock.transform.GetChild(2).GetComponent<Complete_individual>().completed = drawingComplete[d];
        if (drawingComplete[d] == true)
        {
            currentBlock.transform.GetChild(2).GetComponent<Complete_individual>().completed = false;
            currentBlock.transform.GetChild(2).GetComponent<Complete_individual>().CompleteEffect();
            currentBlock.transform.GetChild(2).GetComponent<Complete_individual>().CompleteResult();
        }
    }

    public void WriteToday()
    {
        GameObject.Find("Today").GetComponent<Text>().text = currentDate;
    }
    
}
