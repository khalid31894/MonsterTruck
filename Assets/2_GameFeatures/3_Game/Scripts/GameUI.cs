using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
   public void Home_Btn()
    {
        PlayerPrefsManager.SetCurrentCanvas(0);
        Destroy(Car_Handler.instance.transform.gameObject);
        PaintSecUI.isPainted = false;
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene2_Menu);
    }
    public void Paint_Btn()
    {
        

        Destroy(Car_Handler.instance.transform.gameObject);
        PaintSecUI.isPainted = false;
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene3_Paint);

    }

}
