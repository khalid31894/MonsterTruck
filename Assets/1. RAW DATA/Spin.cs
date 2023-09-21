using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotZ;
    public float speed=100f;
    
    public bool isSpinning=false;



    private void OnTriggerEnter2D(Collider2D collision)
 {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Spin Start!");
            collision.gameObject.transform.SetParent(transform, true);
            //collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
           //collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints2D.FreezePosition;
            isSpinning = true;

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

        if (rotZ >= 360f) { 
            isSpinning = false;
            
            //transform.GetChild(0).gameObject.GetComponent<Rigidbody>().isKinematic = false;
            transform.GetChild(0).gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            transform.GetChild(0).gameObject.GetComponent<Transform>().SetParent(null, true);
        }
        if (isSpinning)
        {
            rotZ += Time.deltaTime * speed;
            transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
        
    }
}
