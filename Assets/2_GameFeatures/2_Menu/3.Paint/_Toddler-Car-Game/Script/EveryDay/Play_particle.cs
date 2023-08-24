using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect_Collision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wheel"))
        {
            transform.GetChild(0).transform.GetComponent<ParticleSystem>().Play();
        }
    }
}
