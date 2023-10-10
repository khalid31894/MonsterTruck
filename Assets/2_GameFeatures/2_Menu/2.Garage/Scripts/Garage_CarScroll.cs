using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Garage_CarScroll : MonoBehaviour
{

    public ScrollRect scrollRect ;
    public float startPos = 2034.109f;
    public float endPos = -2034.109f;

    public float speed = 3f;

    private Tween tween;
    public float delay=1.5f;
    //Sequence sequence;

    public Transform parentGarage;

  
    private void OnEnable()
    {
        /// StartCoroutine(ScollCars()); //for first garage
    }
    private void Start()
    {
       // StartCoroutine(ScollCars());
    }
    private IEnumerator ScollCars()
    {
        yield return new WaitForSeconds(CanvasLoading.delay);
        scrollRect.content.transform.localPosition = new Vector2(endPos, -10.59299f);
        tween = scrollRect.content.transform.DOLocalMoveX(startPos, speed).SetEase(Ease.InOutCubic);
    }
    

    public void ScrollCars()
    {
        Vector2 resetPos = new Vector2(endPos, -10.59299f);
        Sequence sequence = DOTween.Sequence();

        scrollRect.content.transform.localPosition = resetPos;
        if (sequence != null)
        {
            sequence.AppendInterval(delay)
             .Append(
             scrollRect.content.transform.DOLocalMoveX(startPos, speed).SetEase(Ease.InOutCubic)
            );

        }
    }
    
}
