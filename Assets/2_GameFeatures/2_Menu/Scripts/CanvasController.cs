using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    //////////Singleton//////////
    
    private static CanvasController instance;
    public static CanvasController Instance
    {
        get
        {
            if (instance == null)
            {
                // Find the instance in the scene or create a new one
                instance = FindObjectOfType<CanvasController>();

                if (instance == null)
                {
                    // Create a new GameObject to hold the singleton if not found
                    GameObject singletonObject = new GameObject("CanvasController");
                    instance = singletonObject.AddComponent<CanvasController>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        // Ensure there's only one instance
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
    }

    ////////////////////////////

    public int currentCanvas = 0;
    public Canvas[] canvasArray= new Canvas[6];

    public static Action <int> On_CanvasChanger_CallBack;

    public int SetCanvasNumber=1;



    public void ChangeCanvas( int canvasNumber)
    {
        SetCanvasNumber = canvasNumber; //Static var for callback paramerter

        On_CanvasChanger_CallBack = (int canvasNumber) =>
        {
            if (canvasArray == null) { Debug.LogError("Cavas number range should be 0 to 5"); return; };
            canvasArray[canvasNumber].GetComponent<Transform>().gameObject.SetActive(true);                                   //SetActive T Desired Canvas
            canvasArray[PlayerPrefsManager.GetCurrentCanvas()].GetComponent<Transform>().gameObject.SetActive(false);         //SetActive F Current Canvas

            PlayerPrefsManager.SetCurrentCanvas(SetCanvasNumber);
        };


        canvasArray[5].GetComponent<Transform>().gameObject.SetActive(true);


    }


    public static void CanvasChanger_CallBack()
    {
        if (On_CanvasChanger_CallBack != null) { On_CanvasChanger_CallBack(SetCanvasNumber); On_CanvasChanger_CallBack = null; }

    }

}
