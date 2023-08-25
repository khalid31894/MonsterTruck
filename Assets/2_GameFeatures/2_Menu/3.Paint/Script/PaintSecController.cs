using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaintSecController : MonoBehaviour
{
    private void OnEnable()
    {
        CanvasController.LoadAdditiveScene(SceneLoader.Scenes.Scene3_Paint);

    }



}
