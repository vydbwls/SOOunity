using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton2 : MonoBehaviour
{
    private test t;
    private SelectButton1 s1;
    public GameObject button2;
    public GameObject Script2_1;
    public GameObject Script2_2;
    public GameObject Script2_3;

    private void Awake()
    {
        t = FindObjectOfType<test>();
        s1 = FindObjectOfType<SelectButton1>();
    }
    public void IPointerClickHandler()
    {
        if (t.timei == 3) 
        {
            t.timei = 5;
            Script2_1.SetActive(false);
            Script2_2.SetActive(true);
            s1.Script1.SetActive(false);
            s1.Script2.SetActive(true);
        }
        else if (t.timei == 7)
        {
            t.timei = 9;
            Script2_2.SetActive(false);
            Script2_3.SetActive(true);
            s1.Script2.SetActive(false);
            s1.Script3.SetActive(true);
        }
        else if (t.timei == 11)
        {
            t.timei = 13;
        }
        dialog.instance.isSelect_ = false;
    }


}
