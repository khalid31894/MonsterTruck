using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
   public void Home_Btn()
    {
        //IntitializeAdmobAds_CB._instance.ShowAdmobInterstialAd(); // all my ads from  game to main menu


        PlayerPrefsManager.SetCurrentCanvas(0);
        Destroy(Car_Handler.instance.transform.gameObject);
        PaintSecUI.isPainted = false;

        LoaderCallBack.showMeAd = true;
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene2_Menu);
    }
    public void Paint_Btn()
    {
        //IntitializeAdmobAds_CB._instance.ShowAdmobInterstialAd(); // all my ads from  game to paint Sec


        Destroy(Car_Handler.instance.transform.gameObject);
        PaintSecUI.isPainted = false;

        LoaderCallBack.showMeAd = true;

        SceneLoader.LoadScene(SceneLoader.Scenes.Scene3_Paint);

    }

}
