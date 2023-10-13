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
        if (PlayerPrefs.GetInt("RemoveAds") == 1)
        {
            
            foreach(GameObject obj in removeAdsBtn) { obj.SetActive(false); }
        }
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
    public GameObject [] removeAdsBtn;


    public void ChangeCanvas( int canvasNumber)  //Applied on All Btns which change panels
    {
        SoundManager.instance.PlayEffect_Instance(1);

        SetCanvasNumber = canvasNumber; //Static var for callback paramerter

        On_CanvasChanger_CallBack = (int canvasNo) =>
        {
           // if (canvasArray == null || canvasNo < 0 || canvasNo > 5) { Debug.LogError("Cavas number range should be 0 to 5"); return; };
            canvasArray[PlayerPrefsManager.GetCurrentCanvas()].SetActive(false);         //SetActive F Current Canvas
            canvasArray[canvasNo].SetActive(true);                                   //SetActive T Desired Canvas

            PlayerPrefsManager.SetCurrentCanvas(SetCanvasNumber);
        };
        canvasArray[4].SetActive(true);
    }


    public static void CanvasChanger_CallBack()
    {
        if (On_CanvasChanger_CallBack != null) { On_CanvasChanger_CallBack(SetCanvasNumber); On_CanvasChanger_CallBack = null; }
    }



    private void OnEnable()
    {
       if( PaintSecUI.isPainted == true)
        {
            PlayerPrefsManager.SetCurrentCanvas(3);

            canvasArray[0].SetActive(false);


            canvasArray[3].SetActive(true);
        }
        else
        {
            PlayerPrefsManager.SetCurrentCanvas(0);
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
