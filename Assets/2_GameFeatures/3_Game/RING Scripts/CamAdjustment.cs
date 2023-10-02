using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamAdjustment : MonoBehaviour
{
    public Controller_Ring controller_Ring;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            initialCamY = 0.12f;
            targetCamY = 0.45f;
            targetFOV =  74;
            initialFOV = 60;
            StartFOVTransition();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            initialCamY = 0.45f;
            targetCamY = 0.12f;
            targetFOV = 60;
            initialFOV = 74;
            StartFOVTransition();
        }
    }

    public float initialCamY=0.12f;
    public float targetCamY=0.45f;

    public float targetFOV = 74;
    public float transitionDuration = 1.5f;

    public float initialFOV=60;
    private float startTime;



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
            controller_Ring.camera.fieldOfView = Mathf.Lerp(initialFOV, targetFOV, t);
            controller_Ring.camera.GetComponent<SmoothFollow2D>().position.y = Mathf.Lerp(initialCamY, targetCamY, t);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        controller_Ring.camera.fieldOfView = targetFOV; // Ensure the FOV is exactly the target FOV when the transition ends

    }














    }
