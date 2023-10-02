using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndCol_Ring : MonoBehaviour
{
    public Controller_Ring controller_Ring;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("End Col");

            controller_Ring.isReverse = true;
            controller_Ring.isApplyingForce = false;
            controller_Ring.end_Col.enabled = false;

          
        }

    }
}
