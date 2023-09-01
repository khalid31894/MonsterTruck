using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpinMe : MonoBehaviour
{
    public Transform Ring;
    public Transform CenterObj;

    public Vector3 TargetRot;
    public Vector3 Axis;
    public float Angle;

    public float t;
    bool check = false;
    // Start is called before the first frame update
    void Start()
    {
       
        print("Pos: "+transform.position);
        print("Rot: " + transform.rotation);
        print("Forward: " + transform.forward);
        print("From Ring to Ceneter : " + Vector3.Angle(Ring.position,CenterObj.position));
    }

    public void DoSomething()
    {
       // transform.RotateAround(TargetRot, Axis, Angle);
        //float i=Angle;

       // transform.RotateAround(TargetRot, Axis, Mathf.Lerp(i,Angle,10f));
    }

    // Update is called once per frame
    void Update()
    {
        if(check)
        {
            float i = Angle;
            transform.RotateAround(TargetRot, Axis, Mathf.Lerp(i, Angle, 10f));
            check= true;
        }
    }
}
