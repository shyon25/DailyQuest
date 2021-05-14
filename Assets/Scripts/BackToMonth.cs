using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BackToMonth : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData data)
    {
        GameObject.Find("Quests").GetComponent<Quests>().SaveData();
        SceneManager.LoadScene("Month");
    }
}
