using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CalendarManager : MonoBehaviour
{
    public GameObject day;
    GameObject currentBlock;
    public string now;

    public int year;
    public int month;
    public int[] daysOfMonth = new int[12]{ 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

    void Start()
    {
        InitDates();
    }


    void Update()
    {
        
    }

    public void InitDates()//42개의 블럭을 가진 달력 생성
    {
        GameObject.Find("Date").GetComponent<Text>().text = GameObject.Find("Quests").GetComponent<Quests>().currentDate.Substring(0, 7);
        now = GameObject.Find("Date").GetComponent<Text>().text;
        year = int.Parse(now.Substring(0, 4));
        month = int.Parse(now.Substring(5, 2));

        int nowDate = 0;
        for (int j = 0; j < 6; j++)
            for (int i = 0; i < 7; i++)
            {
                currentBlock = GameObject.Instantiate(day);
                currentBlock.name = i.ToString() + j.ToString();
                currentBlock.transform.SetParent(GameObject.Find("Calendar").transform);
                currentBlock.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
                currentBlock.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
                SetFour(currentBlock.GetComponent<RectTransform>(), 140*i, 840-140*i, 170*j, 850-170*j);
                currentBlock.transform.GetChild(0).GetComponent<Text>().text = "";
                if (j * 7 + i >= setDates() && j * 7 + i < dayOfMonths(month, year) + setDates())//시작요일부터 시작해서 끝번호+시작요일까지
                {
                    currentBlock.transform.GetChild(0).GetComponent<Text>().text = (nowDate+1).ToString();
                    nowDate += 1;
                }
            }

    }

    public int setDates() //yyyy-MM 2021년 5월 1일은 토요일이다. 1: 월, 0: 일
    {
        int dayWeek = 6;
        int currentMonth = 5;
        int currentYear = 2021;
        if(year > 2021 || (year == 2021 && month > 5))
        {
            while (currentMonth != month || currentYear != year)
            {
                
                if (currentMonth == 12)
                {
                    dayWeek += dayOfMonths(currentMonth, currentYear) % 7;
                    currentYear += 1;
                    currentMonth = 1;
                }
                else
                {
                    dayWeek += dayOfMonths(currentMonth, currentYear) % 7;
                    currentMonth += 1;
                }
            }
        }
        else if(year < 2021 || (year == 2021 && month < 5))
        {
            while (currentMonth != month || currentYear != year)
            {
                if (currentMonth == 1)
                {
                    dayWeek -= dayOfMonths(12, currentYear) % 7;
                    currentYear -= 1;
                    currentMonth = 12;
                }
                else
                {
                    dayWeek -= dayOfMonths(currentMonth - 1, currentYear) % 7;
                    currentMonth -= 1;
                }
            }
        }

        while (dayWeek < 0)
            dayWeek += 7;
        dayWeek %= 7;
        
        return dayWeek;
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

    public void SetFour(RectTransform rt, float left, float right, float top, float bottom)
    {
        rt.SetLeft(left);
        rt.SetRight(right);
        rt.SetTop(top);
        rt.SetBottom(bottom);
    }

    public int dayOfMonths(int d, int cy)
    {
        int days = 0;

        days = daysOfMonth[d-1];
        if (isLeap(cy) && d == 2)
        {
            days = 29;
        }

        return days;
    }
}
