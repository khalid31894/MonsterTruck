using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SceneLoader;
using UnityEngine.SceneManagement;

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
    public GameObject [] canvasArray= new GameObject[6];

    public static Action <int> On_CanvasChanger_CallBack;

    public static  int SetCanvasNumber;



    public void ChangeCanvas( int canvasNumber)  //Applied on All Btns which change panels
    {
        SetCanvasNumber = canvasNumber; //Static var for callback paramerter

        On_CanvasChanger_CallBack = (int canvasNumber) =>
        {
            if (canvasArray == null || canvasNumber<0 || canvasNumber>5) { Debug.LogError("Cavas number range should be 0 to 5"); return; };
            canvasArray[canvasNumber-1].SetActive(false);         //SetActive F Current Canvas
            canvasArray[canvasNumber].SetActive(true);                                   //SetActive T Desired Canvas

            PlayerPrefsManager.SetCurrentCanvas(SetCanvasNumber);
        };
        canvasArray[5].SetActive(true);
    }


    public static void CanvasChanger_CallBack()
    {
        if (On_CanvasChanger_CallBack != null) { On_CanvasChanger_CallBack(SetCanvasNumber); On_CanvasChanger_CallBack = null; }
    }



    private void OnEnable()
    {
       if( PaintSecUI.isPainted == true)
        {
            canvasArray[4].SetActive(true);
        }
    }


    ///////////////////////////////////////


    //private void Start()
    //{
    //    IsGameSceneToMenuScene();
    //}

    //private void IsGameSceneToMenuScene()
    //{
    //    if (PlayerPrefsManager.GetCurrentCanvas() ==3) //if paint btn was clicked in game play
    //    {
    //        canvasArray[5].SetActive(true); //active loading
    //        canvasArray[0].SetActive(false); //main panel off 
    //        canvasArray[3].SetActive(true);  //paint panel on

    //    }
    //    else
    //    {

    //    }

    //}


    /////////////////////////////////////////////////
    ///






    //public static void LoadAdditiveScene(Scenes scene)
    //{

    //        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);



    //}

    //public static void UnLoadAdditiveScene(Scenes scene)
    //{

    //        SceneManager.UnloadSceneAsync(scene.ToString());

    //}











}
