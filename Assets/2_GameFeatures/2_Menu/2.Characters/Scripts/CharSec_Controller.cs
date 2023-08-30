using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharSec_Controller : MonoBehaviour
{
   
    public void CharSelection(int selection)
    {
        PlayerPrefsManager.SetCurrentChar(selection);
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene3_Paint);
        //CanvasController.Instance.ChangeCanvas(3);
    }
}
