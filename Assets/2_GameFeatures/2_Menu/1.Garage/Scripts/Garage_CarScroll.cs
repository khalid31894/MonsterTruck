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

    public Action slideCars_CallBack;
    void Start()
    {
        slideCars_CallBack = () => {
            scrollRect.content.transform.localPosition = new Vector2(endPos, -10.59299f);
            tween = scrollRect.content.transform.DOLocalMoveX(startPos, speed).SetEase(Ease.InOutCubic);
        };
    }
    private void OnEnable()
    {
        /// StartCoroutine(ScollCars()); //for first garage
    }
    private IEnumerator ScollCars()
    {
        yield return new WaitForSeconds(CanvasLoading.delay);
        scrollRect.content.transform.localPosition = new Vector2(endPos, -10.59299f);
        tween = scrollRect.content.transform.DOLocalMoveX(startPos, speed).SetEase(Ease.InOutCubic);
    }
    private void OnDisable()
    {
        tween.Kill();
    }
}
