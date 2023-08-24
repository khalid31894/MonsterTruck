using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ReNew : MonoBehaviour
{
    public Select_Module selectModule;
    public float Car_Force;
    public BoxCollider2D SpeedBoster;
    public void Rest()
    {
        GetComponent<Animator>().enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (selectModule == Select_Module.Baloon)
        {
            if (collision.gameObject.CompareTag("CharacterPart") && gameObject.CompareTag("ReNew"))
            {
               // SoundManager.instance.PlayEffect_Instance(17);
                AnimatorHandler.Instance.PlaySmile();
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                Invoke(nameof(Resting), 6f);
            }
        }
        else if (selectModule == Select_Module.Box)
        {
            if (collision.gameObject.CompareTag("CoinTrigger"))
            {
                AnimatorHandler.Instance.PlaySmile();
                //SoundManager.instance.PlayEffect_Instance(16);
                GetComponent<Rigidbody2D>().AddForce(Vector2.right* 50, ForceMode2D.Impulse);
                gameObject.layer = 11;
            }
        } 
        else if (selectModule == Select_Module.bakra)
        {
            if (collision.gameObject.CompareTag("CoinTrigger"))
            {
              //  SoundManager.instance.PlayEffect_Complete(13);
            }
        }
        else if (selectModule == Select_Module.vlc)
        {
            if (collision.gameObject.CompareTag("CoinTrigger"))
            {
                AnimatorHandler.Instance.PlaySmile();
               // SoundManager.instance.PlayEffect_Complete(18);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(5,15) * 50, ForceMode2D.Force);
                gameObject.layer = 11;
            }
        }
        else if (selectModule == Select_Module.Murga)
        {
            if (collision.gameObject.CompareTag("CoinTrigger"))
            {
             //   SoundManager.instance.PlayEffect_Complete(14);
            }
        }
        else if (selectModule == Select_Module.end)
        {
            if (collision.gameObject.CompareTag("CoinTrigger"))
            {
               // SoundManager.instance.PlayEffect_Instance(26);
                transform.GetChild(0).gameObject.SetActive(true);
            }
        } 
        else if (selectModule == Select_Module.dino)
        {
            if (collision.gameObject.CompareTag("CoinTrigger"))
            {
                int r = Random.Range(1, 3);
               
                GetComponent<Animator>().Play(r.ToString());
                if (r == 1)
                {
                   // SoundManager.instance.PlayEffect_Instance(62);
                }
                else
                {
                   // SoundManager.instance.PlayEffect_Instance(63);
                }
            }
        } 
        
        else if (selectModule == Select_Module.Ball)
        {
            if (collision.gameObject.CompareTag("CoinTrigger"))
            {
                
                AnimatorHandler.Instance.PlaySmile();
               // SoundManager.instance.PlayEffect_Instance(21);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(5, 15) * 100, ForceMode2D.Force);
                gameObject.layer = 11;
            }
        }
        else if (selectModule == Select_Module.rocket)
        {
            if (collision.gameObject.CompareTag("CoinTrigger"))
            {
               // SoundManager.instance.PlayEffect_Instance(61);
                GetComponent<Animator>().enabled = true;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }

        else if (selectModule == Select_Module.Star)
        {
            if (collision.gameObject.CompareTag("CharacterPart") || collision.gameObject.CompareTag("CoinTrigger"))
            {
                AnimatorHandler.Instance.PlaySmile();
                //SoundManager.instance.PlayEffect_Instance(22);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
            }
        }
        else if (selectModule == Select_Module.Gift)
        {
            if (collision.gameObject.CompareTag("CharacterPart"))
            {
                AnimatorHandler.Instance.PlaySmile();
               // SoundManager.instance.PlayEffect_Instance(19);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                GetComponent<Collider2D>().enabled = false;
                Invoke(nameof(AgainGift), 5);
            }
        }
        else if (selectModule == Select_Module.Water)
        {
            if (collision.gameObject.CompareTag("Wheel"))
            {
                AnimatorHandler.Instance.PlaySmile();
               // SoundManager.instance.PlayEffect_Instance(20);
                transform.GetChild(0).transform.GetComponent<ParticleSystem>().Play();
            }
        }
        else if (selectModule == Select_Module.Speed_Boster)
        {
            if (collision.gameObject.CompareTag("Wheel"))
            {
                AnimatorHandler.Instance.PlayConfuse();
               // SoundManager.instance.PlayEffect_Instance(25);
               //SoundManager.instance.PlayEffect_Instance(53);
                Debug.Log("Add Force on Car");
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                //Vector2 relativeVelocity = GetComponent<Rigidbody2D>().velocity - Car_Handler.instance.controller.GetComponent<Rigidbody2D>().velocity;
                //Vector2 force = relativeVelocity * -Car_Force;
                // Add the force to the object's Rigidbody2D component
                Car_Handler.instance.controller.GetComponent<Rigidbody2D>().AddForce(Vector2.right*750, ForceMode2D.Impulse);
            }
        }
        else if (selectModule == Select_Module.SpeedBosterReset)
        {
            if (collision.gameObject.CompareTag("Wheel"))
            {
                Debug.Log("Add Force on Car");
                SpeedBoster.enabled = true;
            }
        } 
       
        
    }
    void AgainGift()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }
    void Resting()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}


public enum Select_Module
{
    Baloon,
    Box,
    Gift,
    Star,
    Water,
    Speed_Boster,
    SpeedBosterReset,
    end,
    bakra,
    Murga,
    Ball,
    vlc,rocket,dino

};
