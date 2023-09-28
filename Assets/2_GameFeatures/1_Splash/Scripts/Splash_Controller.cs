using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash_Controller : MonoBehaviour
{
    [SerializeField] private GameObject PrivacyPanel;

    [SerializeField] private float SplashDelay=3;


    public GameObject splashPanel;


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
            splashPanel.SetActive(true);
            StartCoroutine(ChangeScene());
        }
    }

    private IEnumerator ChangeScene() {
        yield return new WaitForSeconds(SplashDelay);
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene2_Menu);
    }


}
