using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharSec_Controller : MonoBehaviour
{
   
    public void CharSelection(int selection)
    {
        PlayerPrefsManager.SetCurrentChar(selection);
        StartCoroutine(CharSelectTime());

    }

    IEnumerator CharSelectTime()
    {
        SoundManager.instance.PlayEffect_Instance(1);

        yield return new WaitForSeconds(1);

       
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene3_Paint);
        //CanvasController.Instance.ChangeCanvas(3);
    }
}
