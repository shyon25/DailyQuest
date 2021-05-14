using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataController : MonoBehaviour
{
    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }
    static DataController _instance;
    public static DataController Instance
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public string FileName = "quests.json";

    public Quests loadingData;
    public Quests savingData
    {
        get
        {
            if (loadingData == null)
            {
                LoadData();
                SaveData();
            }
            return loadingData;
        }
    }
    private void Start()
    {
        LoadData();
        SaveData();
    }

    public void LoadData()
    {
        string filepath = Application.persistentDataPath + FileName;
        if (File.Exists(filepath))
        {
            string FromJsonData = File.ReadAllText(filepath);
            loadingData = JsonUtility.FromJson<Quests>(FromJsonData);
        }
        else
        {
            loadingData = new Quests();
        }
    }

    public void SaveData()
    {
        string ToJsonData = JsonUtility.ToJson(savingData);
        string filepath = Application.persistentDataPath + FileName;
        File.WriteAllText(filepath, ToJsonData);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

}
