using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
   public void Home_Btn()
    {
        PlayerPrefsManager.SetCurrentCanvas(0);
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene2_Menu);
    }
    public void Paint_Btn()
    {
        PlayerPrefsManager.SetCurrentCanvas(3);
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene2_Menu);

    }

}
