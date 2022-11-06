using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
//Talk, Quest--------------------------------------
    public bool isTalk;

    public TalkManager talkManager;
    public QuestManager questManager;

    public GameObject DialoguePannel;
    public Image portraitImg;
    public Text DialogueText;
    public Text nameText;
    public GameObject scanObject;
    public int talkIndex;

    //Event--------------------------------------
    public GameObject EventDialoguePannel;
    public Text EventText;
    public int Eventindex;
    public bool isEvent;
    public EventManager eventmanager;

    //QuestUI------------------------------------
    public Text QuestName;
    public string Qname;

    //Object interactiron----------------------------------
    public InteractionManager interactionmanager;

    void Awake()
    {
        isTalk = false;
    }

    private void Start()
    {
        Event();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isEvent)
        {
            Event();
        }

        Qname = questManager.CheckQuest();
        if (questManager.CheckQuest() == null)
        {
            QuestName.text = null;
        }
        else
        {
            QuestName.text = Qname;
        }
    }

    public void Action(GameObject scanobj)
    {
        scanObject = scanobj;
        ObjData objData = scanobj.GetComponent<ObjData>();
        Talk(objData.id, objData.isNpc);
        
        DialoguePannel.SetActive(isTalk);
        portraitImg.gameObject.SetActive(isTalk);
        interactionmanager.TransObj(objData.id, isTalk);
    }

    void Talk(int id, bool isNpc)
    {
        int questTalkIndex = questManager.GetQuestTalkIndex(id);
        string talkData = talkManager.GetTalk(id+ questTalkIndex, talkIndex);

        if(talkData == null)
        {
            isTalk = false;
            talkIndex = 0;
            Debug.Log(questManager.CheckQuest(id));
            return;
        }
        
        if (isNpc)
        {
            DialogueText.text = talkData.Split(':')[0];
            
            portraitImg.sprite = talkManager.GetPortrait(id, int.Parse (talkData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }   

        else
        {
            DialogueText.text = talkData;

            portraitImg.color = new Color(1, 1, 1, 0);
        }

        isTalk = true;
        talkIndex++;
    }   

    public void Event()
    {
        string eventData = eventmanager.GetEvent(Eventindex);
        EventDialoguePannel.SetActive(isEvent);
        EventText.text = eventData;
        //Debug.Log("¿Ã∫•∆Æ");
        //eventmanager.GetEvent();

        if (eventData == null)
        {
            isEvent = false;
            Eventindex = 0;
            EventDialoguePannel.SetActive(isEvent);
            return;
        }

        else
        {
            Eventindex += 1;
        }
    }
}
