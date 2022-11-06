using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InteractObject
{
    public int itemId;

    public bool isOrigin; //텍스트 출력 끝나고 원상복구 해야되면 true
    public bool isTransform; //트랜스폼 된 상태인지 아닌 지 확인(isOrigin == false 이면 변경 후 원래대로 안 돌림)
    public GameObject original;
    public GameObject transform;
}
public class InteractionManager : MonoBehaviour
{
    [SerializeField] InteractObject[] interobj = null;
    public void TransObj(int scanid, bool istalk)
    {
        Debug.Log("입성");
        for (int i = 0; i < interobj.Length; i++)
        {
            if (interobj[i].itemId == scanid)
            {
                if (!interobj[i].isTransform)
                {
                    Debug.Log(scanid);
                    interobj[i].original.SetActive(false);
                    interobj[i].transform.SetActive(true);
                    interobj[i].isTransform = true;
                }
            
                else if(interobj[i].isOrigin && !istalk)
                {
                    OriginObj(scanid, i);
                }
            }

        }
        
    }

    public void OriginObj(int scanid, int i)
    {        
         Debug.Log("입성");
         
         interobj[i].original.SetActive(true);
         interobj[i].transform.SetActive(false);
         interobj[i].isTransform = false;                      
    }
}
