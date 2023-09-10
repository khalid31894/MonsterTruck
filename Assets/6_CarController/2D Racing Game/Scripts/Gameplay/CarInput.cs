using UnityEngine;
using System.Collections;

public class CarInput : MonoBehaviour {


	[HideInInspector]public CarController carController;

	IEnumerator Start ()
	{
		yield return new WaitForSeconds (.3f);
        carController = Car_Handler.instance.controller;
    }

	public void Gas ()
	{
        if (carController != null)
        {
            carController.Acceleration();
            carController.forward = true;
            if (Grounded.Uneven)
            {
                SoundManager.instance.PlayEffect_Loop(57);
            }
        }
    }

	public void Brake ()
	{
        if (carController != null)
        {
            if (AnimatorHandler.Instance.idle && Grounded.IsGrounded)
            {
                AnimatorHandler.Instance.CarBack();
            }
          SoundManager.instance.PlayEffect_Loop(12);
            carController.Brake();
            carController.backward = true;
        }
    }

	public void ReleaseGasBrake ()
	{
        if (carController != null)
        {
            if (!carController.backward)
            {
                AnimatorHandler.Instance.SetIdle();
            }

            carController.Down = true;
            carController.GasBrakeRelease();
        }
    }
}
