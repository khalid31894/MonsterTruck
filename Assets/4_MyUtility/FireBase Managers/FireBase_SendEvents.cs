using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Analytics;


public class FireBase_SendEvents : MonoBehaviour
{
    private void OnEnable()
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(gameObject.name + "_Completed");
        Debug.Log(gameObject.name + "_Completed");

        if (SceneManager.GetActiveScene().name == "Scene4_Game")
        {
            // if(Firebase!=null)
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Track : " + PlayerPrefsManager.GetCurrentTrack());
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Garage : " + PlayerPrefsManager.GetCurrentGarage());
            Firebase.Analytics.FirebaseAnalytics.LogEvent("Monster Truck : " + PlayerPrefsManager.GetCurrentTruck());

        }
    }
}
