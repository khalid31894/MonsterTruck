using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class GarageSlider : MonoBehaviour
{
   

    public float closeEndPos = 0f;
    public float openEndPos = 0f;
    public float time = 1f;

    
    private void OnEnable()
    {
        //On_OpenGarage_Callback = () =>
        //{
        //    Debug.Log("Open");
        //    this.gameObject.transform.DOLocalMoveY(openEndPos, time);
        //};
        //On_CloseGarage_Callback = () =>
        //{
        //    Debug.Log("Close");
        //    this.gameObject.transform.DOLocalMoveY(closeEndPos, time);
        //};




    }
    private void OnDisable()
    {
        
    }
    public void OpenGarage()
    {
        this.gameObject.transform.DOLocalMoveY(openEndPos, time);
    }
    public void CloseGarage()
    {
        this.gameObject.transform.DOLocalMoveY(closeEndPos, time);
    }



}
