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
        talkData.Add(10 + 2000, new string[] { "어휴:2", "아휴:0", "왜 한숨이야:1", "조금있다 말하자:2" });
        talkData.Add(11 + 3000, new string[] { "왔냐:2", "아휴:0", "또 한숨이야:1", "조금 더 있다 말하자:2" });
        talkData.Add(12 + 4000, new string[] { "??뭔일:3", "쟤네 왜저럼?:1", "몰루? 뭐 잃어버린 듯:3", "ㅇㅎ:1" });
        
        talkData.Add(20 + 7000, new string[] { "이현이 이름이네 이건가?:1", "이현이의 잃어버린 종이를 찾았다." });
        talkData.Add(21 + 3000, new string[] { "엥, 헐 ㄳ:0" });

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
