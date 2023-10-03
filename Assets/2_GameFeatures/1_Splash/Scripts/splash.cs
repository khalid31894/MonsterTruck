using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class splash : MonoBehaviour
{
    public GameObject laregeScreen, smallScreen, tabletScreen;
   
    float aspect;
    void Awake()
    {
        Application.targetFrameRate= 60;
        aspect = (float)Screen.width / (float)Screen.height;
        aspect = (float)Math.Round(aspect, 2);
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Input.multiTouchEnabled = false;
    }
    private void Start()
    {
        StartCoroutine(setSplashSize());
    }
   
 
    IEnumerator setSplashSize()
    {
        if (aspect <=1.6 )
        {
            Debug.Log("Tab: "+aspect);
            tabletScreen.SetActive(true);
        }
        else if (aspect == 2.09 || aspect == 1.78 || (aspect >= 1.7f && aspect < 1.8f))
        {
            Debug.Log("small: " + aspect);
           
            smallScreen.SetActive(true);
        }
        else if(aspect>2)
        {
            Debug.Log("Large: " + aspect);
            laregeScreen.SetActive(true);

        }
        else
        {
            Debug.Log("Else: " + aspect);
            laregeScreen.SetActive(true);
        }
      
        yield return null;
    }
    

}


