using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButton1 : MonoBehaviour
{
    private test t;
    private SelectButton2 s2;
    public GameObject button1;
    public GameObject Script1;
    public GameObject Script2;
    public GameObject Script3;

    private void Awake()
    {
        t = FindObjectOfType<test>();
        s2 = FindObjectOfType<SelectButton2>();
    }
    public void IPointerClickHandler()
    {
        if (t.timei == 3) 
        {
            t.timei = 3;
            Script1.SetActive(false);
            Script2.SetActive(true);
            s2.Script2_1.SetActive(false);
            s2.Script2_2.SetActive(true);
        }
        else if (t.timei==7)
        {
            t.timei = 7;
            Script2.SetActive(false);
            Script3.SetActive(true);
            s2.Script2_2.SetActive(false);
            s2.Script2_3.SetActive(true);
        }
        else if (t.timei == 11)
        {
            t.timei = 11;
        }
        dialog.instance.isSelect_ = false;
    }
    
}
