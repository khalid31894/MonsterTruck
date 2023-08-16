using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;
public class ContinuousCarMotion : MonoBehaviour
{
    public CarController carController;

    public GameObject EndPos;
    public Transform StartPos;


    bool isInMotion;

    void OnEnable() { StartCoroutine(StartMotion()); }

    IEnumerator StartMotion()
    {
        yield return new WaitForSeconds(1.5f);
        isInMotion = true;

    }

    // Start is called before the first frame update
 

    // Update is called once per frame
    void Update()
    {
        if (isInMotion)
        {
            carController.Acceleration();
        }
    }



    ///Bring to Start Pos

    private static Action onStartAgain;



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) { 
        other.gameObject.transform.position=StartPos.position;
        }
    }



}
