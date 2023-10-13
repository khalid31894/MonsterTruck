using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CharSec_Controller : MonoBehaviour
{
    //public GameObject loadingPanel;
    public Animator Char_animator;

    public void Home_Btn()
    {
        CanvasController.Instance.ChangeCanvas(0);

    }


    public void CharSelection_Btn(int selection)
    {
        PlayerPrefsManager.SetCurrentChar(selection);
        // StartCoroutine(CharSelectTime());
        StartCoroutine(CharSelectFlow());
        

    }


    IEnumerator CharSelectFlow()
    {
        if(PlayerPrefsManager.GetCurrentChar()==1) { Char_animator.Play("Dog_Waving"); }
        if(PlayerPrefsManager.GetCurrentChar()== 2) { Char_animator.Play("Cat_Waving"); }
        if(PlayerPrefsManager.GetCurrentChar()==3) { Char_animator.Play("Waving"); }
        
        yield return new WaitForSeconds(2);
        CanvasController.Instance.ChangeCanvas(2);
    }
    private void OnDisable()
    {
        Char_animator.Play("Preview");
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
