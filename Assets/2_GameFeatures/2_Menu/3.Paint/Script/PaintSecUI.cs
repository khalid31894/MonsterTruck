using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaintSecUI : MonoBehaviour
{
    public static bool isPainted = false;

    public void GoToTrackSelection()
    {


        SoundManager.instance.PlayEffect_Instance(0);
        DOTween.KillAll();
        Car_Handler.instance.controller.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        Car_Handler.instance.DisAbleParticleRigid();
        GameObject C = GameObject.FindWithTag("DontDestroy");
        Car_Handler.instance.flag = false;
        if (C != null)
        {
            Debug.Log("Not Null" + C.name);
        }
        else
        {
            Debug.Log("It is Null");
        }
        C.transform.SetParent(null);
        Car_Handler.instance.Car = C;
        C.SetActive(false);
        IndieStudio.DrawingAndColoring.Logic.GameManager.PaintMInstance.mCamera.clearFlags = CameraClearFlags.Depth;
        DontDestroyOnLoad(Car_Handler.instance.Car);
      //  SceneManager.LoadSceneAsync(SceneName);



        //CanvasController.Instance.ChangeCanvas(4);
        //CanvasController.UnLoadAdditiveScene(SceneLoader.Scenes.Scene3_Paint);


        isPainted = true;
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene2_Menu);






    }
}
