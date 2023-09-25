using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCol_Ring : MonoBehaviour
{
    public Controller_Ring controller_Ring;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            controller_Ring.rb = collision.GetComponent<Rigidbody2D>();
            controller_Ring.isApplyingForce = true;
            controller_Ring.isReverse = false;

            controller_Ring.startRamp.enabled = true;
            controller_Ring.mid_Col.enabled = true;
        }
    }
    private void FixedUpdate()
    {
        if (controller_Ring.isApplyingForce)
        {
            controller_Ring.ApplyCentrifugalForce();
        }
    }
    
}
