using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidCol_Ring : MonoBehaviour
{
    public Controller_Ring controller_Ring;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Mid Col");
            if (!controller_Ring.isReverse)
            {
                controller_Ring.start_Col.enabled = false;
                controller_Ring.end_Col.enabled   = true;
                controller_Ring.startRamp.enabled = false;
                controller_Ring.endRamp.enabled   = true;
            }
            else
            {
                controller_Ring.start_Col.enabled = true;

                controller_Ring.end_Col.enabled    =  false;

                controller_Ring.endRamp.enabled      =  false;
                controller_Ring.startRamp.enabled    =  true;
            }

        }

    }


  
}
