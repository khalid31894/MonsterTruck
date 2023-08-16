using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CanvasLoading : MonoBehaviour
{
    public float delay = 3;


    public void OnEnable() 
    {
        StartCoroutine(Delay());
    }
    IEnumerator Delay()
    {
        CanvasController.CanvasChanger_CallBack();
        yield return new WaitForSeconds(delay);
        
        this.gameObject.SetActive(false);
    }

}
