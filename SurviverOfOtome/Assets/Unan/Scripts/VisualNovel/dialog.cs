﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class dialog_info
{
    public string name;
    [TextArea(3, 5)]
    public string content;
    public bool check_read;
    public Sprite R_face_info;
    public Sprite L_face_info;
    public bool isBGM;
    public Sprite Background;
    public Sprite SpriteCG;
    public bool isSFX;
    public bool isSelect;
    public bool isFadeout;
    public bool isFadein;
}



[System.Serializable]
public class Dialog_cycle
{
    public string cycle_name;
    public List<dialog_info> info = new List<dialog_info>();
    public int cycle_index;
    public bool check_cycle_read;
}

public class dialog : MonoBehaviour
{

    [SerializeField]
    public static dialog instance = null;
    public List<Dialog_cycle> dialog_cycles = new List<Dialog_cycle>(); //대화 지문 그룹
    public Queue<string> text_seq = new Queue<string>();                //대화 지문들의 내용을 큐로 저장한다.(끝점을 쉽게 판단하기 위해)
    public string name_;                                                //임시로 저장할 대화 지문의 이름
    public string text_;                                                //임시로 저장할 대화 지문의 내용
    public GameObject mouse;
    public GameObject select;
    public GameObject fadeout;
    public GameObject fadein;


    public Text nameing;                                                //대화 지문 오브젝트에 있는 것을 표시할 오브젝트
    public Text DialogT;                                                //대화 지문 내용 오브젝트
    public Text Next_T;                                               //다음 버튼
    public GameObject dialog_obj;                                       //대화 지문 오브젝트
    public Image L_face_info_;
    public Image R_face_info_;
    public Image SpriteCG_;
    public Image Background_;
    public bool IsImage;
    public bool isSelect_;
    public bool isFadeOut_;
    public bool isFadeIn_;
    IEnumerator seq_;
    IEnumerator skip_seq;

    public float delay;
    public bool running = false;
    test t;
    void Awake()    //싱글톤 패턴으로 어느 씬에서든 접근 가능하게 한다.
    {
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);

        t = FindObjectOfType<test>();


        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        mouse.SetActive(IsImage);
        select.SetActive(isSelect_);
        fadeout.SetActive(isFadeOut_);
        fadein.SetActive(isFadeIn_);
    }

    public IEnumerator dialog_system_start(int index) //다이얼로그 출력 시작
    {
        nameing = dialog_obj.GetComponent<parameter>().name_text;   //다이얼로그 오브젝트에서 각 변수 받아오기
        DialogT = dialog_obj.GetComponent<parameter>().content;
        Next_T = dialog_obj.GetComponent<parameter>().next_text;
        
        running = true;
        foreach (dialog_info dialog_temp in dialog_cycles[index].info)  //대화 단위를 큐로 관리하기 위해 넣는다.
        {
            text_seq.Enqueue(dialog_temp.content);
        }

        dialog_obj.gameObject.SetActive(true);
        for (int i = 0; i < dialog_cycles[index].info.Count; i++) //대화 단위를 순서대로 출력
        {
            nameing.text = dialog_cycles[index].info[i].name;

            text_ = text_seq.Dequeue();                                  //대화 지문을 pop

            seq_ = seq_sentence(index, i);                               //대화 지문 출력 코루틴
            StartCoroutine(seq_);

            if (dialog_cycles[index].info[i].isBGM)
            {
                soundManager.instance.PlaySoundBGM();
            }
            if (dialog_cycles[index].info[i].isSFX)
            {
                soundManager.instance.PlaySoundSFX();
            }
            if (dialog_cycles[index].info[i].isSelect)
            {
                isSelect_ = dialog_cycles[index].info[i].isSelect;
            }
            if (dialog_cycles[index].info[i].isFadeout)
            {
                isFadeOut_ = dialog_cycles[index].info[i].isFadeout;
            }
            if (dialog_cycles[index].info[i].isFadein)
            {
                isFadeIn_ = dialog_cycles[index].info[i].isFadein;
            }

            if (dialog_cycles[index].info[i].L_face_info)
            {
                L_face_info_ = dialog_obj.transform.Find("L_face_info").GetComponent<Image>();
                L_face_info_.sprite = dialog_cycles[index].info[i].L_face_info;
            }
            if (dialog_cycles[index].info[i].R_face_info)
            {
                R_face_info_ = dialog_obj.transform.Find("R_face_info").GetComponent<Image>();
                R_face_info_.sprite = dialog_cycles[index].info[i].R_face_info;
            }

            if (dialog_cycles[index].info[i].Background)
            {
                Background_ = dialog_obj.transform.Find("Background").GetComponent<Image>();
                Background_.sprite = dialog_cycles[index].info[i].Background;
            }
            if (dialog_cycles[index].info[i].SpriteCG)
            {
                SpriteCG_ = dialog_obj.transform.Find("SpriteCG").GetComponent<Image>();
                SpriteCG_.sprite = dialog_cycles[index].info[i].SpriteCG;
            }


            yield return new WaitUntil(() =>
            {
                if (dialog_cycles[index].info[i].check_read)            //현재 대화를 읽었는지 아닌지
                {
                    return true;                                        //읽었다면 진행
                }
                else
                {
                    return false;                                       //읽지 않았다면 다시 검사
                }
            });
        }

        dialog_cycles[index].check_cycle_read = true;
        running = false;
    }

    public void DisplayNext(int index, int number)                      //다음 지문으로 넘어가기
    {
        Next_T.text = "";
        Next_T.gameObject.SetActive(false);

        if (text_seq.Count == 0)                                      //다음 지문이 없다면
        {
            dialog_obj.gameObject.SetActive(false);                     //다이얼로그 끄기
        }

        StopCoroutine(seq_);                                            //실행중인 코루틴 종료
        dialog_cycles[index].info[number].check_read = true;            //현재 지문 읽음으로 표시
    }

    public IEnumerator seq_sentence(int index, int number)              //지문 텍스트 한글자식 연속 출력
    {
        skip_seq = touch_wait(seq_, index, number);                     //터치 스킵을 위한 터치 대기 코루틴 할당
        StartCoroutine(skip_seq);                                       //터치 대기 코루틴 시작
        DialogT.text = "";                                              //대화 지문 초기화
        foreach (char letter in text_.ToCharArray())                    //대화 지문 한글자씩 뽑아내기
        {
            DialogT.text += letter;                                     //한글자씩 출력
            yield return new WaitForSeconds(delay);                     //출력 딜레이
        }
        Next_T.gameObject.SetActive(true);
        Next_T.text = "next";
        StopCoroutine(skip_seq);                                        //지문 출력이 끝나면 터치 대기 코루틴 해제
        IEnumerator next = next_touch(index, number);                   //버튼 이외에 부분을 터치해도 넘어가는 코루틴 시작
        StartCoroutine(next);
    }

    public IEnumerator touch_wait(IEnumerator seq, int index, int number)//터치 대기 코루틴
    {
        yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        StopCoroutine(seq);                                              //대화 지문 코루틴 해제
        DialogT.text = text_;                                            //스킵시 모든 지문 한번에 출력
        Next_T.gameObject.SetActive(true);
        Next_T.text = "next";
        IEnumerator next = next_touch(index, number);                    //대화 지문 코루틴 해제 됬기 때문에 다음 지문으로 가는 코루틴 시작
        StartCoroutine(next);
    }

    public IEnumerator next_touch(int index, int number)    //터치로 다음 지문 넘어가는 코루틴
    {
        
        IsImage = true;
        StopCoroutine(seq_);
        StopCoroutine(skip_seq);
        yield return new WaitForSeconds(0.3f);
        if (isSelect_)
        {
            float t = 5f;
            yield return new WaitForSeconds(t);
            t += 1f;
        }
        if (isFadeOut_)
        {
            float t = 1.5f;
            yield return new WaitForSeconds(t);
            t += 1f;
        }
        if (t.timei == 4)
        {
            t.timei = 6;
        }
        if (t.timei == 8)
        {
            t.timei = 10;
        }
        if (t.timei == 12)
        {
            t.timei = 14;
        }
        yield return new WaitUntil(() => Input.GetMouseButton(0));
        IsImage = false;
        DisplayNext(index, number);
    }

    public bool dialog_read(int check_index)          //index의 부분을 읽었는지 확인하는 함수
    {
        if (!dialog_cycles[check_index].check_cycle_read)
        {
            return true;
        }

        return false;
    }
}

