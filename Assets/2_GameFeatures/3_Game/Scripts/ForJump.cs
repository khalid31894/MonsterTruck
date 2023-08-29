using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForJump : MonoBehaviour
{
    public GameObject jump;
    public float up;
    public float down;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CoinTrigger") && Grounded.IsGrounded)
        {
            AnimatorHandler.Instance.idle = true;
            AnimatorHandler.Instance.PlaySmile();
            Car_Handler.instance.controller.IsJump = false;
            GameManager.instance.Jump.interactable = false;
           // SoundManager.instance.PlayEffect_Complete(15);
            jump.transform.DOLocalMoveY(-up, 0.3f).OnComplete(() =>
            {
                jump.transform.DOLocalMoveY(down, 0.1f); 
            });
            Debug.Log(collision.gameObject.name);
            Car_Handler.instance.controller.GetComponent<Rigidbody2D>().AddForce(transform.up * 650, ForceMode2D.Impulse);
        }
    }
}
