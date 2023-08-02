using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash_Initialization : MonoBehaviour
{
    public bool can_Initialize_SDK;
    public float Initializing_Time=3f;



    
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

 

}


