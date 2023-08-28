using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dreamteck;
using Dreamteck.Splines;
using UnityEngine.Experimental.Playables;
using DG.Tweening;

public class Wheel : MonoBehaviour
{
    SplineComputer spline;
    CarController CC;
    public float speed = 10;
    public GameObject StartP,EndP;
    private void Start()
    {
        spline=transform.GetComponentInParent<SplineComputer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="CoinTrigger"&&gameObject.name=="start")
        {
            Car_Handler.instance.controller.ringF = true;
            SmoothFollow2D.Instance.carController.Using = true;
            v = collision.gameObject;
            v.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            v.transform.parent.gameObject.AddComponent<SplineFollower>();
            SplineFollower s= v.transform.GetComponentInParent<SplineFollower>();
            s.updateMethod=SplineUser.UpdateMethod.FixedUpdate;
            s.spline = spline;
            s.motion.is2D= true;
            s.followSpeed = 25;
            s.useTriggers   = true;
            //s.triggerGroup = 1;
            EndP.SetActive(true);
            gameObject.SetActive(false) ;
            SmoothFollow2D.Instance.MaxOrthoSize = 10;
        }
        if (collision.tag == "CoinTrigger" && gameObject.name == "Active")
        {
            StartP.SetActive(true);
        }
    }

    public void stopCam()
    {
        Camera.main.DOOrthoSize(8.3f, 2f);
        AnimatorHandler.Instance.PlaySmile();
        //SmoothFollow2D.Instance.enabled = false;
    }
    public void startCam()
    {
        Camera.main.DOOrthoSize(7, 2f);
        SmoothFollow2D.Instance.MaxOrthoSize = 7;
        //SmoothFollow2D.Instance.enabled = true;
    }
    public float x, y;
    GameObject v;
    public void AddForce()
    {
        AnimatorHandler.Instance.PlaySmile();
        SmoothFollow2D.Instance.enabled = true  ;
        SmoothFollow2D.Instance.MaxOrthoSize = 7;
        print("asdad");
        SmoothFollow2D.Instance.carController.Using = false;
        Car_Handler.instance.controller.ringF = false;
        v.transform.parent.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        CC = v.GetComponentInParent<CarController>();
        CC.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y), ForceMode2D.Impulse);
        Destroy(v.transform.parent.gameObject.GetComponent<SplineFollower>());
    } 
    public void addSpeedBoost()
    {
        AnimatorHandler.Instance.PlayConfuse();
        //Car_Handler.instance.controller.gameObject.GetComponent<SplineFollower>().followSpeed += 2;
    }
}
