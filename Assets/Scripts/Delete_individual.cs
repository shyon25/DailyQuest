using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Delete_individual : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData data)
    {
        DeleteEffect();
        DeleteResult();
        GameObject.Find("Quests").GetComponent<Quests>().SaveData();
    }

    public void DeleteEffect()
    {

    }

    public void DeleteResult()
    {
        int myNumber = int.Parse(gameObject.transform.parent.GetChild(0).name);
        GameObject.Find("EventManager").GetComponent<QuestsManager>().quests.RemoveAt(myNumber);
        GameObject.Find("EventManager").GetComponent<QuestsManager>().dates.RemoveAt(myNumber);
        GameObject.Find("EventManager").GetComponent<QuestsManager>().completes.RemoveAt(myNumber);
        GameObject.Find("EventManager").GetComponent<QuestsManager>().Saveit();
    }
}
