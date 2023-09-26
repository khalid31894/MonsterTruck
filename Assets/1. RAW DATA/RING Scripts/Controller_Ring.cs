using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Controller_Ring : MonoBehaviour
{
    public float centrifugalForce = 100f;

    public Camera camera;

    public Collider2D start_Col;
    public Collider2D mid_Col;
    public Collider2D end_Col;

    public Collider2D startRamp;
    public Collider2D endRamp;

    public bool isReverse;



    public Rigidbody2D rb;

    public bool isApplyingForce;

    public void ApplyCentrifugalForce()
    {

        if (isApplyingForce)
        {
            Vector2 centrifugalForceVector = -transform.up * centrifugalForce;

            rb.AddForce(centrifugalForceVector);
            Debug.Log(rb.totalForce.y);
        }
    }


    public IEnumerator CameraMove()
    {
        //camera.transform.DOLocalMoveZ(-30, 1.5f);
        camera.GetComponent<SmoothFollow2D>().position.y = 0.40f;
        //camera.GetComponent<SmoothFollow2D>().position.y = Mathf.Lerp(0.12f, 0,0.40f, Time.deltaTime * 10);

       // isNear = true;
        yield return new WaitForSeconds(3.5f);
       // isNear = false;
        //camera.transform.DOLocalMoveZ(-19, 1);
        camera.GetComponent<SmoothFollow2D>().position.y = 0.12f;
    }

    //public void lerpCam()
    //{
    //    camera.GetComponent<SmoothFollow2D>().position.y = Vector2.Lerp(new Vector2(0, 0.12f), new Vector2(0, 0.40f), Time.deltaTime * 10).y;

    //}
    //public void lerpCamBack()
    //{
    //    camera.GetComponent<SmoothFollow2D>().position.y = Vector2.Lerp(new Vector2(0, 0.12f), new Vector2(0, 0.40f), Time.deltaTime * 10).y;

    //}

    //public bool isNear;
    //public float camSpeed=0.5f;
    //private float lerpTime = 0.0f;
    //private void FixedUpdate()
    //{
    //    lerpTime += Time.deltaTime * camSpeed;
    //  // lerpTime= Mathf.Clamp01(lerpTime);

    //    if (isNear) {
    //        float targetY = Mathf.Lerp(0.12f, 0.40f, lerpTime);
    //        camera.GetComponent<SmoothFollow2D>().position =     new Vector2(camera.GetComponent<SmoothFollow2D>().position.x, targetY);
    //    }
    //    if (!isNear)
    //    {
    //        camera.GetComponent<SmoothFollow2D>().position.y = Mathf.Lerp(0.40f, 0.12f, lerpTime);
    //    }

    //}






    private void Start()
    {
        if (Camera.main != null)
        {
            camera = Camera.main;


        }
    }



}
