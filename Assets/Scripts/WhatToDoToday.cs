using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class WhatToDoToday : MonoBehaviour
{
    Quests Quests;
    string now;

    // Start is called before the first frame update
    void Start()
    {
        Quests = GameObject.Find("Quests").GetComponent<Quests>();
        now = DateTime.Now.ToString("yyyy-MM-dd");
        Whatday();
        MakeToday();
    }
    public void Whatday()
    {
        GameObject.Find("Today").GetComponent<Text>().text = now;
    }

    public void MakeToday()
    {
        int allCount = Quests.quests.Count;
        int currentCount = 0;
        string whatToDo = "";

        for(int i=0; i<allCount; i++)
        {
            if(Quests.date[i] == now)
            {
                whatToDo += Quests.quests[i];
                if (Quests.completes[i] == true)
                {
                    whatToDo += "(��)";
                    currentCount--;
                }
                whatToDo += "\n";
                currentCount++;
            }
        }
        if (currentCount == 0)
            whatToDo = "������ �� ���� ����.\n" + whatToDo;
        else
            whatToDo = "������ �� �� : " + currentCount.ToString() + "��\n" + whatToDo;

        GameObject.Find("ToDo").transform.GetChild(0).GetComponent<Text>().text = whatToDo;
    }
}
