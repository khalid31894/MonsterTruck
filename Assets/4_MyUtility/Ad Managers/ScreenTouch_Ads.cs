using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ScreenTouch_Ads : MonoBehaviour
{
    public static ScreenTouch_Ads instance;
    public float Delay_Time;
    public float get_touch = 0;
    public bool IsPlay_Count;
    private void Awake()
    {
        print("in timer");
        instance = this;
        Input.multiTouchEnabled = false;
        IsPlay_Count = true;
        //Delay_Time=20;
       // AssignAdIds_CB.instance.play_Ad = false;
    }
    void Update()
    {
        if (IsPlay_Count && PlayerPrefs.GetInt("unlockall") == 0)
        {
            get_touch += Time.deltaTime;
            if (get_touch >= Delay_Time)
            {
                AssignAdIds_CB.instance.play_Ad = true;
                IsPlay_Count = false;
            }
        }
        else if (IsPlay_Count && PlayerPrefs.GetInt("UnlockGame") == 1)
        {
            get_touch += Time.deltaTime;
            if (get_touch >= Delay_Time)
            {
                AssignAdIds_CB.instance.play_Ad = true;
                IsPlay_Count = false;
            }
        }
    }
}
