using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotZ;
    public float speed = 100f;

    public bool isSpinning = false;

    public Rigidbody2D car;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Debug.Log("Spin Start!");
            // collision.gameObject.transform.SetParent(transform, true);
            // //collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            ////collision.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints2D.FreezePosition;
            // isSpinning = true;
            car= collision.GetComponent<Rigidbody2D>();

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

        //    //if (rotZ >= 360f) { 
        //    //    isSpinning = false;

        //    //    //transform.GetChild(0).gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //    //    //transform.GetChild(0).gameObject.GetComponent<Rigidbody2D>().constraints=RigidbodyConstraints2D.None;
        //    //    if (transform.GetChild(0))
        //    //    transform.GetChild(0).gameObject.GetComponent<Transform>().SetParent(null, true);
        //    //}
        //    if (isSpinning)
        //    {

        //        rotZ += Time.deltaTime * speed;
        //        transform.rotation = Quaternion.Euler(0, 0, rotZ);


        //        if (rotZ >= 360f)
        //        {
        //            isSpinning = false;

        //            if (transform.GetChild(0))
        //            transform.GetChild(0).gameObject.GetComponent<Transform>().SetParent(null, true);
        //        }
        

    }
    private void FixedUpdate()
    {
        if (rotZ >= 360f)
        {
             isSpinning = false;
        }
        if(isSpinning )
        {

        }



    }
}

