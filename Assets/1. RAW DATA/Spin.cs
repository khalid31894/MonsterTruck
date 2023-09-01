using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CompareTag("Player"))
        {
            Debug.Log("Working!");
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
