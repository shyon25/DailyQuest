using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class Quests : MonoBehaviour
{
    public List<string> quests;
    public List<string> date;
    public List<bool> completes;
    public string currentDate;
    public List<string> completedDates;
    
    void Start()
    {
        currentDate = DateTime.Now.ToString(("yyyy-MM-dd"));
        quests = new List<string>();
        date = new List<string>();
        completes = new List<bool>();
        completedDates = new List<string>();

        LoadData();
        SaveData();

        DontDestroyOnLoad(gameObject);
    }

    public void SaveData()
    {
        PlayerPrefs.DeleteAll();

        List<int> completes_int = new List<int>();
        for(int i=0; i<completes.Count; i++)
        {
            if (completes[i] == true)
                completes_int.Add(1);
            else
                completes_int.Add(0);
        }
        for(int i=0; i<quests.Count; i++)
        {
            PlayerPrefs.SetString("quests" + i, quests[i]);
            PlayerPrefs.SetString("date" + i, date[i]);
            PlayerPrefs.SetInt("completes" + i, completes_int[i]);
        }
        for(int i = 0; i<completedDates.Count; i++)
        {
            PlayerPrefs.SetString("completedDates" + i, completedDates[i]);
        }
    }

    public void LoadData()
    {
        List<int> completes_int = new List<int>();
        int keyCount = 0;
        while(PlayerPrefs.HasKey("quests" + keyCount) == true)
        {
            quests.Add(PlayerPrefs.GetString("quests" + keyCount));
            date.Add(PlayerPrefs.GetString("date" + keyCount));
            completes_int.Add(PlayerPrefs.GetInt("completes" + keyCount));
            keyCount++;
        }
        keyCount = 0;
        while (PlayerPrefs.HasKey("completedDates" + keyCount) == true)
        {
            completedDates.Add(PlayerPrefs.GetString("completedDates" + keyCount));
            keyCount++;
        }
        for (int i = 0; i < completes_int.Count; i++)
        {
            if (completes_int[i] == 1)
                completes.Add(true);
            else
                completes.Add(false);
        }

    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

}
