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

    private void Start()
    {
        if (Camera.main != null)
        {
            camera = Camera.main;


        }
    }

}
