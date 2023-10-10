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
