using IndieStudio.DrawingAndColoring.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UIElements;

public class Car_Handler : MonoBehaviour
{
    public GameObject Car;
    public bool flag = false;
    public ShapesCanvas PreviousP;
    public Rigidbody2D[] rigidComponent;
    //public GameObject coinsTrigger;
    public RawReferenceCar rawData;
    public CarController controller;
    public float CarScale=42;
    public static Car_Handler instance;
    public Animator[] clips;

    public WheelJoint2D[] total;
    public JointMotor2D[] motor;
    public Animator Belt;
    void Start()
    {
        controller= GetComponentInChildren<CarController>();
        rawData=GetComponentInChildren<RawReferenceCar>();
        rawData.GetComponent<Canvas>().sortingLayerName= "Car";
        rawData.GetComponent<Canvas>().sortingOrder= 2;
        instance = this;
    }
    public void chkFlag()
    {
        flag = true;
        if (flag)
        {
            //working
            //total = controller.motorWheel;
            //motor = new JointMotor2D[total.Length];
            //motor[0] = total[0].motor;
            //motor[1] = total[1].motor;
            controller.ended = false;
            controller.finishGateReached = false;
            controller.TargetAchieved = false;
            controller.Stopcar = false;
            PreviousP.SetP(Car);
        }
    }
    //private void FixedUpdate()
    //{
    //    if (flag == true /*&& Mathf.Abs(motor[0].motorSpeed) > 10*/)
    //    {
    //        print(Mathf.Abs(motor[0].motorSpeed));
    //        motor[0].motorSpeed = Mathf.Lerp(motor[0].motorSpeed, 0, 100000);
    //        motor[1].motorSpeed = Mathf.Lerp(motor[1].motorSpeed, 0, 100000);
    //        total[0].motor = motor[0];
    //        total[1].motor = motor[1];
    //    }
    //}
    public void addParticleRigid()
    {
        var v = rawData.DecorationP;
        var Manager = IndieStudio.DrawingAndColoring.Logic.GameManager.PaintMInstance;
        for (int i = 0; i < v.transform.childCount; i++)
        {
            var c = v.transform.GetChild(i);
            c.gameObject.AddComponent<Rigidbody2D>().gravityScale = 0;
            c.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

            if (c.GetComponent<Animator>())
            {
                c.GetComponent<Animator>().enabled = false;

            }
            var d = c.GetComponent<Drag_particle_obj>();
            int myInt = int.Parse(d.pName);
            d.handler = Manager.handlers[myInt];
        }
        for (int i= 0; i < clips.Length; i++)
        {
            clips[i].enabled = false;
        }
        controller.IsGamePlay = false;
    }
    public void DisAbleParticleRigid()
    {
        var v = rawData.DecorationP;
        for (int i = 0; i < v.transform.childCount; i++)
        {
            var c = v.transform.GetChild(i);
            Destroy(c.GetComponent<Rigidbody2D>());
            if (c.GetComponent<Animator>())
            {
                c.GetComponent<Animator>().enabled = true;
            }
        }
        for (int i = 0; i < clips.Length; i++)
        {
            clips[i].enabled = true;
        }
        controller.IsGamePlay = true;
        
    }
}
