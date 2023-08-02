using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash_Controller : MonoBehaviour
{
    [SerializeField] private GameObject PrivacyPanel;

    [SerializeField] private float SplashDelay=3;


    void Start()
    {
        CanShowPrivacyPanel();


    }

    

    public void CanShowPrivacyPanel()
    {
        if (PlayerPrefsManager.GetPrivacySplash()==false)
        {
        PrivacyPanel.SetActive(true);
        }
        else
        {
            StartCoroutine(ChangeScene());
        }
    }

    private IEnumerator ChangeScene() {
        yield return new WaitForSeconds(SplashDelay);
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene3_Menu);
    }


}
