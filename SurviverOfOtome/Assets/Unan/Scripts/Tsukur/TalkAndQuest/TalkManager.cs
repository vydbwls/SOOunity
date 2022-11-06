using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;
    public string[] portraitName;

    // Start is called before the first frame update
    void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        //Talk Data

        talkData.Add(2000, new string[] { "ㅎㅇ:0", "처음왔구나:2" });
        talkData.Add(3000, new string[] { "어휴:2", "아휴:0" });

        for (int i = 0; i < portraitArr.Length; i++)
        {
            portraitData.Add(2000 + i, portraitArr[i]);
            portraitData.Add(3000 + i, portraitArr[i]);
            portraitData.Add(4000 + i, portraitArr[i]);
        }


        //Quest Talk Data
        talkData.Add(9000, new string[] { "비타민음료, 얼음팩, 도시락...?" });
        talkData.Add(10 + 3000, new string[] { "침대 맡의 협탁이다. 아래쪽에 가방이 놓여있다.", "조사 해보자", "(종이봉투, 필통, 휴대폰, 지갑… 그리고…)", "…?",
            "이건 뭐지?", "(아무튼 귀중품들이 들어있다.)", "(당연하게도 휴대폰을 먼저 확인했다.)", "….꺼져있네.", "(나는 지갑을 꺼내서 확인했다.)", "(이건.. 학생증인가...?)",
            "…주변이 너무 어두워서 잘 안보여", "불빛을 찾아보자." });
        talkData.Add(11 + 9000, new string[] { "냉장고 불빛으로 가방 안에 있던 물건을 확인해 볼 수 있을 것 같아." });

    }

    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
            return talkData[id][talkIndex];
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}
