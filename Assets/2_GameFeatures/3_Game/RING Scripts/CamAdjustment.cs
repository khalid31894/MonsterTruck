using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAdjustment : MonoBehaviour
{
    public float initialCamY=0.25f;
    public float targetCamY=0.45f;

    public float targetFOV = 74;
    public float initialFOV=60;

    public float transitionDuration = 1.5f;


    private float startTime;
    public Controller_Ring controller_Ring;


    private float temp_initialCamY;
    private float temp_targetCamY;
    private float temp_targetFOV;
    private float temp_initialFOV;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //initialCamY = 0.25f;
            //targetCamY = 0.45f;
            //targetFOV =  74;
            //initialFOV = 60;

            temp_initialCamY = initialCamY;
            temp_targetCamY = targetCamY;
            temp_targetFOV = targetFOV;
            temp_initialFOV = initialFOV;
            StartFOVTransition();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //initialCamY = 0.45f;
            //targetCamY = 0.25f;
            //targetFOV = 60;
            //initialFOV = 74;

            temp_initialCamY = targetCamY;
            temp_targetCamY = initialCamY;
            temp_targetFOV = initialFOV;
            temp_initialFOV = targetFOV;

            StartFOVTransition();
        }
    }


    private void StartFOVTransition()
    {
        startTime = Time.time;
        StartCoroutine(ChangeFOV());
    }

    private IEnumerator ChangeFOV()
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionDuration)
        {
            float t = elapsedTime / transitionDuration;
           // controller_Ring.camera.fieldOfView = Mathf.Lerp(temp_initialFOV, temp_targetFOV, t);
            controller_Ring.camera.GetComponent<SmoothFollow2D>().position.y = Mathf.Lerp(temp_initialCamY, temp_targetCamY, t);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

       // controller_Ring.camera.fieldOfView = targetFOV; // Ensure the FOV is exactly the target FOV when the transition ends

    }


    }
