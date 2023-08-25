using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public  enum Scenes
    {
        Scene1_Splash,
        Scene2_Menu,
        Scene3_Paint,
        Scene4_Game,
        Scene5_Loading,
    }

    private static Action onLoaderCallBack;


    public static void LoadScene(Scenes scene)
    {
        onLoaderCallBack = () =>
        {
            SceneManager.LoadScene(scene.ToString());
        };
        SceneManager.LoadScene(Scenes.Scene5_Loading.ToString());
    }
    public static void LoaderCallBack() { 
    
    if(onLoaderCallBack != null)
        {
            onLoaderCallBack();
            onLoaderCallBack=null;
        }
    }



    //public static void LoadAdditiveScene(Scenes scene)
    //{
    //    onLoaderCallBack = () =>
    //    {
    //        SceneManager.LoadScene(scene.ToString(),LoadSceneMode.Additive);
    //    };
    //    SceneManager.LoadScene(Scenes.Scene5_Loading.ToString());
    //}

    //public static void UnLoadAdditiveScene(Scenes scene)
    //{
    //    onLoaderCallBack = () =>
    //    {
    //        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Additive);

    //        SceneManager.UnloadSceneAsync(scene.ToString());
    //    };
    //    SceneManager.LoadScene(Scenes.Scene5_Loading.ToString());
    //}

}

