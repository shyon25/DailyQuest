using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GotoDay : MonoBehaviour, IPointerClickHandler
{
    public Texture daybutton_none;
    public Texture daybutton_exist;
    public Texture daybutton_complete;
    public string myName;

    public void OnPointerClick(PointerEventData data)
    {
        if (gameObject.transform.GetChild(0).GetComponent<Text>().text != "")
        {
            GameObject.Find("Quests").GetComponent<Quests>().currentDate = FindMyName();
            SceneManager.LoadScene("Day");
        }
    }

    void Update()
    {
        if (gameObject.transform.GetChild(0).GetComponent<Text>().text != "")
        {
            changeImage();
        }
    }

    public void changeImage()
    {
        List<string> date = GameObject.Find("Quests").GetComponent<Quests>().date;
        List<bool> completes = GameObject.Find("Quests").GetComponent<Quests>().completes;

        myName = FindMyName();
        int counting = 0;
        int counting_true = 0;

        for (int i=0; i<date.Count; i++)
        {
            if(date[i] == myName)
            {
                counting += 1;
                if (completes[i] == true)
                    counting_true += 1;
            }
        }
        
        if (counting == 0)
            gameObject.GetComponent<RawImage>().texture = daybutton_none;
        else if(counting == counting_true && GameObject.Find("Quests").GetComponent<Quests>().completedDates.Contains(myName))
            gameObject.GetComponent<RawImage>().texture = daybutton_complete;
        else
            gameObject.GetComponent<RawImage>().texture = daybutton_exist;
        
    }

    public string FindMyName()
    {
        string myName = "";

        myName = gameObject.transform.GetChild(0).GetComponent<Text>().text;
        if (int.Parse(myName) >= 10)
            myName = GameObject.Find("Date").GetComponent<Text>().text + "-" + myName;
        else if (int.Parse(myName) < 10)
            myName = GameObject.Find("Date").GetComponent<Text>().text + "-0" + myName;

        return myName;
    }
}
