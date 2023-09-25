using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        yield return new WaitForSeconds(4);
        //camera.transform.DOLocalMoveZ(-19, 1);
        camera.GetComponent<SmoothFollow2D>().position.y = 0.12f;
    }
    private void Start()
    {
        if (Camera.main != null)
        {
            camera = Camera.main;


        }
    }



}
