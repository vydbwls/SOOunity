using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestObject
{
    public int questid;
    public List<isQuest> quest = new List<isQuest>();
    public int questOk;
}

[System.Serializable]
public class isQuest
{
    public int itemId;
    public bool isCheck;
    public bool isTrigger;
    public int Iindex;
}

public class QuestManager : MonoBehaviour
{
    [SerializeField] QuestObject[] questobj = null;

    public int questId;
    public int questActionIndex;
    public GameObject[] questObject;
    Dictionary<int, QuestData> questList;

    // Start is called before the first frame update
    void Awake()
    {
        questList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    void GenerateData()
    {
        questList.Add(10, new QuestData("양호실 조사", new int[] {3000, 9000}));
        questList.Add(20, new QuestData("무거", new int[] { }));
    }

    public int GetQuestTalkIndex(int id)
    {
        return questId + questActionIndex;
    }

    public string CheckQuest(int id)
    {
        if (id == questList[questId].npcId[questActionIndex])
            questActionIndex++;

        ControlObject();

        if (questActionIndex == questList[questId].npcId.Length)
        {
            questActionIndex--;
            //NextQuest();
        }
        return questList[questId].questName;
    }

    public string CheckQuest()
    {
        return questList[questId].questName;
    }

    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }

    void ControlObject()
    {
        switch(questId)
        {
            case 10:
                break;

            case 20:
                break;

            case 30:
                break;
        }
    }

    public void CheckObj(int scanid, int questindex)
    {
        for (int i = 0; i < questobj[questindex].quest.Count; i++)
        {
            if (questobj[questindex].quest[i].itemId == scanid)
            {

                Debug.Log("마장");
                if (questobj[questindex].quest[i].isCheck == false)
                {
                    if (questobj[questindex].quest[i].isTrigger == false)
                    {                                           
                        questobj[questindex].questOk += 1;      
                    }                                                   

                    else
                    {
                        questActionIndex = questobj[questindex].quest[i].Iindex;
                    }
                    questobj[questindex].quest[i].isCheck = true;
                }
            }

        }
    }
}
