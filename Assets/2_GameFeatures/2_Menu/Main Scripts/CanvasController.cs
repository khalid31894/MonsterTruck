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

    public static  int SetCanvasNumber;



    public void ChangeCanvas( int canvasNumber)  //Applied on All Btns which change panels
    {
        SetCanvasNumber = canvasNumber; //Static var for callback paramerter

        On_CanvasChanger_CallBack = (int canvasNumber) =>
        {
            if (canvasArray == null || canvasNumber<0 || canvasNumber>4) { Debug.LogError("Cavas number range should be 0 to 5"); return; };
            canvasArray[PlayerPrefsManager.GetCurrentCanvas()].GetComponent<Transform>().gameObject.SetActive(false);         //SetActive F Current Canvas
            canvasArray[canvasNumber].GetComponent<Transform>().gameObject.SetActive(true);                                   //SetActive T Desired Canvas

            PlayerPrefsManager.SetCurrentCanvas(SetCanvasNumber);
        };
        canvasArray[5].GetComponent<Transform>().gameObject.SetActive(true);
    }


    public static void CanvasChanger_CallBack()
    {
        if (On_CanvasChanger_CallBack != null) { On_CanvasChanger_CallBack(SetCanvasNumber); On_CanvasChanger_CallBack = null; }
    }



    ///////////////////////////////////////




    private void Start()
    {
        IsGameSceneToMenuScene();
    }

    private void IsGameSceneToMenuScene()
    {
        if (PlayerPrefsManager.GetCurrentCanvas() ==3) //if paint btn was clicked in game play
        {
            canvasArray[5].GetComponent <Transform>().gameObject.SetActive(true); //active loading
            canvasArray[0].GetComponent<Transform>().gameObject.SetActive(false); //main panel off 
            canvasArray[3].GetComponent<Transform>().gameObject.SetActive(true);  //paint panel on
          
        }
        else
        {
           
        }

    }




}
