using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Analytics;


public class Splash_Initialization : MonoBehaviour
{
    public bool can_Initialize_SDK;
    public float Initializing_Time=3f;


    public FireBase_Initializer fireBase_Initializer;



    
    private void Awake()
    {
        if (can_Initialize_SDK)
        {
            
            StartCoroutine(Initialize_SDKs());




        }
        
    }

   


   private IEnumerator Initialize_SDKs()
    {
        Debug.Log("Initialize_SDKs");
        //Initialize SDK here



        yield return new WaitForSeconds(Initializing_Time);


    }

    void InitializeFirebase()
    {
        if (can_Initialize_SDK)
        {
            fireBase_Initializer.FireBase_Initializer_Call();
        }
    }



 

}


