using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Active_Ring : MonoBehaviour
{
   public  Controller_Ring controller_Ring;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            controller_Ring.start_Col.enabled = true;
           // controller_Ring.startRamp.enabled = true;
        }
    }
}
