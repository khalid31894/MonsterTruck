using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashUI_Controller : MonoBehaviour
{
    public GameObject splashPanel;
    public GameObject privacyPanel;

   public void PrivacyAccept_Btn()
    {
        privacyPanel.SetActive(false);
        splashPanel.SetActive(true);
        StartCoroutine(splashWait());
    
    }
    public void PrivacyPolicy_Btn()
    {
        //Direct to URL
        Application.OpenURL("https://taptoy.io/privacy/");
    }
    private IEnumerator splashWait()
    {

        yield return new WaitForSeconds(3);
        PlayerPrefsManager.SetPrivacySplash(true);
       // SceneLoader.LoadScene(SceneLoader.Scenes.Scene2_Menu);
        SceneManager.LoadScene(nameof(SceneLoader.Scenes.Scene2_Menu));

    }


}
