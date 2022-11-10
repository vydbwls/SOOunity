using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    List<Dictionary<string, object>> text;

    public GameObject[] EventImage;
    public int id;
    public int EventIndex;

    public CSVReader CSVReader;
    public GameObject scanEventObject;

    public GameManager theGame;

    //questmanager 변수 관리-----------------------
    QuestManager questManager;
    int SetQuestId;

    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
        EventIndex = 0;
        GenerateData();
    }

    public void GenerateData()
    {
        if (id == 1)
        {
            text = CSVReader.Read("Tutorial1");
        }

        else if (id == 2)
        {
            text = CSVReader.Read("Tutorial2");
        }
    }
    public string GetEvent(int eventindex)
    {
        GenerateData();
        EventIndex = eventindex;
        if (EventIndex == text.Count)
        {
            EndEvent();
            return null;
        }
        else
        {
            return ((string)text[EventIndex]["Content"]);
        }

    }

    public void QuestAction()
    {

    }

    public void EndEvent()
    {
        if(id == 1)
        {
            questManager.questId = 10;
        }
    }


}
