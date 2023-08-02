using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashUI_Controller : MonoBehaviour
{
   public void PrivacyAccept_Btn()
    {
      
            PlayerPrefsManager.SetPrivacySplash(true);
            SceneLoader.LoadScene(SceneLoader.Scenes.Scene3_Menu);
    
    }
    public void PrivacyPolicy_Btn()
    {
        //Direct to URL
        Application.OpenURL("https://taptoy.io/privacy/");
    }



}
