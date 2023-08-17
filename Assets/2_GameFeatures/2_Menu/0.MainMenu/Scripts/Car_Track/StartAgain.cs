using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAgain : MonoBehaviour
{
    /// <summary>
    /// Restart Car Motion
    /// </summary>
    public Transform StartPos;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit, Get:  "+ collision.gameObject.transform.parent.parent.name);

            //collision.gameObject.transform.parent.GetComponent<CarController>().stop = true;
            collision.gameObject.transform.parent.position = StartPos.position;
           // collision.gameObject.transform.parent.GetComponent<CarController>().stop = false;

        }
    }
}
