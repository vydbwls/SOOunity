﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{
    public float time = 100f;
    public float target;
    public float[] targetA;
    public Text time_text;
    public bool test_1;
    public int timei;
    public bool isTime;

    private void Update()
    {
        if (timei <= targetA.Length)
        {
            isTime = true;
            if (isTime)
            {
                if (test_1)
                {
                    StartCoroutine("timer");
                }
                else
                {
                    StartCoroutine("timer_action");
                }
            }
        }
    }

    public void Re(int index)
    {
        timei = index;
    }

    IEnumerator timer()
    {
        isTime = false;
        yield return new WaitUntil(() => {
            if (time <= 0)
            {
                return true;
            }
            else
            {

                if (time <= target)
                {
                    if (dialog.instance.dialog_read(0) && !dialog.instance.running)
                    {
                        IEnumerator dialog_co = dialog.instance.dialog_system_start(0);
                        StartCoroutine(dialog_co);

                        if (dialog.instance.dialog_read(0))
                        {
                            return false;
                        }

                    }
                    else if (!dialog.instance.dialog_read(0) && !dialog.instance.running)
                    {
                        time -= Time.deltaTime;
                        time_text.text = time.ToString();
                    }
                }
                else
                {
                    time -= Time.deltaTime;
                    time_text.text = time.ToString();
                }

                return false;
            }
        });
    }
    IEnumerator timer_action()
    {
        yield return new WaitUntil(() => {
            if (time <= 0)
            {
                return true;
            }
            else
            {
                
                if (time <= targetA[timei] && time >= targetA[timei+1])
                {
                    if (dialog.instance.dialog_read(timei) && !dialog.instance.running)
                    {
                        IEnumerator dialog_co = dialog.instance.dialog_system_start(timei);
                        StartCoroutine(dialog_co);

                        if (dialog.instance.dialog_read(timei))
                        {
                            timei++;
                            return false;
                        }
                        
                    }
                    else if (!dialog.instance.dialog_read(timei) && !dialog.instance.running)
                    {
                        time -= Time.deltaTime;
                        time_text.text = time.ToString();
                    }

                }

                else
                {
                    time -= Time.deltaTime;
                    time_text.text = time.ToString();
                }
                
                return false;
            }
            
        });
    }

}
