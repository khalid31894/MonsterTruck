using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class Grounded : MonoBehaviour
{
    public static bool IsGrounded = true,Uneven=false;
    public bool check;
    private void Update()
    {
        check = IsGrounded;
    }
    private void Start()
    {
        IsGrounded= true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Ground"&&!IsGrounded)
        {
            //SoundManager.instance.PlayEffect_Complete(23);
            IsGrounded = true;
            //SoundManager.instance.StopEffect(57);
            if (GameManager.instance)
            {
                if (Car_Handler.instance.controller.IsJump != true)
                {
                    Car_Handler.instance.controller.IsJump = true;
                    GameManager.instance.Jump.interactable = true;
                   // AnimatorHandler.Instance.PlayLanded();
                }
            }
        }
        if (collision.gameObject.name == "UnEvenRoad")
        {
            print("UNecve"); 
           // SoundManager.instance.PlayEffect_Loop(57);
            Uneven = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
        IsGrounded = false;
        Uneven = false;
        //SoundManager.instance.StopEffect(57);

        if (GameManager.instance && Car_Handler.instance)
        {
            Car_Handler.instance.controller.IsJump = false;
            if (GameManager.instance.Jump)
            {

                GameManager.instance.Jump.interactable = false;
              
            }
        }
    }
}
