using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySimpleTweens : MonoBehaviour
{
    public SimpleTweens selectTween;

    [SerializeField] float scaleBy= 0.01f;
    [SerializeField] float scaleTime = 4f;
    [SerializeField] float perRotationTime = 3f;
    private Vector3 orignalForm;
    Tween x;

    private void OnEnable()
    {
        orignalForm = this.gameObject.transform.localScale ;
        if (selectTween == SimpleTweens.rotateTyre)
        {
           x=this.gameObject.transform.DOLocalRotate(new Vector3(0,0,-360f),perRotationTime,RotateMode.LocalAxisAdd)
                .SetLoops(-1)
                .SetRelative()
                .SetEase(Ease.Linear);
        }
        if(selectTween== SimpleTweens.scale)
        {
           //x= this.gameObject.transform.DOPunchScale(new Vector3(scaleBy,scaleBy,scaleBy), scaleTime).SetLoops(-1);
            x = this.gameObject.transform.DOPunchScale(new Vector3(scaleBy, scaleBy, scaleBy), scaleTime, 0,0).SetLoops(-1).SetEase(Ease.Linear);

        }
    }
    private void OnDisable()
    {
        this.gameObject.transform.localScale= orignalForm;
        x.Kill();

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum SimpleTweens
{
    rotateTyre,
    scale,



}
