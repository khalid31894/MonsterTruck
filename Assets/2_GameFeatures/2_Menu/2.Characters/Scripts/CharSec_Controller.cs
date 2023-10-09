using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharSec_Controller : MonoBehaviour
{
    //public GameObject loadingPanel;
    public void Home_Btn()
    {
        CanvasController.Instance.ChangeCanvas(1);

    }


    public void CharSelection_Btn(int selection)
    {
        PlayerPrefsManager.SetCurrentChar(selection);
       // StartCoroutine(CharSelectTime());

        CanvasController.Instance.ChangeCanvas(2);

    }

    //IEnumerator CharSelectTime()
    //{
    //    SoundManager.instance.PlayEffect_Instance(1); 
    //    loadingPanel.SetActive(true);
    //    yield return new WaitForSeconds(1);
       
       
    //    SceneLoader.LoadScene(SceneLoader.Scenes.Scene3_Paint);
    //    //CanvasController.Instance.ChangeCanvas(3);
    //}
}
