using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHand : MonoBehaviour
{
    public GameObject[] tutorialObject;
    public float interactionTimeout = 5f; // Adjust this to set the timeout duration in seconds.
    private float lastInteractionTime;

    private void Start()
    {
        // Initialize the lastInteractionTime to the current time.
        lastInteractionTime = Time.time;
    }

    private void Update()
    {
        // Check for user interaction (e.g., mouse click or touch).
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            // Interaction detected, reset the timer.
            lastInteractionTime = Time.time;

            // Deactivate the tutorial object.
            tutorialObject[0].SetActive(false);
            tutorialObject[1].SetActive(false);
        }
        else
        {
            // No interaction, check if the timeout has been reached.
            if (Time.time - lastInteractionTime >= interactionTimeout)
            {
                // Timeout reached, activate the tutorial object.
                tutorialObject[0].SetActive(true);
                tutorialObject[1].SetActive(true);
            }
        }
    }
}
