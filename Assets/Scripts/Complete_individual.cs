using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Complete_individual : MonoBehaviour, IPointerClickHandler
{
    public Texture complete;
    public Texture realComplete;

    public bool completed = false;


    public void OnPointerClick(PointerEventData data)
    {
        CompleteEffect();
        CompleteResult();
        GameObject.Find("Quests").GetComponent<Quests>().SaveData();
    }

    public void CompleteEffect()
    {
        if (completed == false)
        {
            gameObject.transform.parent.transform.GetChild(0).GetComponent<Text>().color = Color.gray;
            gameObject.GetComponent<RawImage>().texture = realComplete;
        }
        else if(completed == true)
        {
            gameObject.transform.parent.transform.GetChild(0).GetComponent<Text>().color = Color.black;
            gameObject.GetComponent<RawImage>().texture = complete;
        }
    }

    public void CompleteResult()
    {
        completed = !completed;
        GameObject.Find("EventManager").GetComponent<QuestsManager>().completes[int.Parse(this.gameObject.transform.parent.GetChild(0).name)] = completed;
        GameObject.Find("EventManager").GetComponent<QuestsManager>().Saveit();
    }
}
