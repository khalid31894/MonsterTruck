using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Feautre_PrivacyPanels : MonoBehaviour
{
    public GameObject FirstPanel;
    public GameObject SecondPanel;


   

    private void OnEnable()
    {
        FirstPanel.SetActive(true);
        SecondPanel.SetActive(false);
        
    }
    private void OnDisable ()
    {
        FirstPanel.SetActive(false);
        SecondPanel.SetActive(false);

    }







    public void Buy_Btn()
    {
        FirstPanel.SetActive(true);
        SecondPanel.SetActive(false);

    }
    public void About_Btn()
    {

        SoundManager.instance.PlayEffect_Instance(1);

        FirstPanel.SetActive(false);
        SecondPanel.SetActive(true);

    }
    public void Terms_Btn()
    {
        SoundManager.instance.PlayEffect_Instance(1);

        Application.OpenURL("https://taptoy.io/terms-of-service");
    }
    public void Privacy_Btn()
    {
        SoundManager.instance.PlayEffect_Instance(1);

        Application.OpenURL("https://taptoy.io/privacy/");
    }

    public void RateUs_Btn()
    {
        SoundManager.instance.PlayEffect_Instance(1);

        Application.OpenURL("https://apps.apple.com/us/app/toddler-world-preschool-games/id6451037660");

    }


    public void Exit_Btn()
    {
        SoundManager.instance.PlayEffect_Instance(1);

        gameObject.SetActive(false);
    }

}
