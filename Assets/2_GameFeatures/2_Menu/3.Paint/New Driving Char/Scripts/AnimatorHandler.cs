using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public static AnimatorHandler Instance;
    public Animator Player;
    public bool idle = true;
    private void OnEnable()
    {
        Instance = this;
        Player = GetComponentInChildren<Animator>();
  

    }
    public void SetIdle()
    {
        if (!idle)
        {
            idle = true;
         //   print("Idle");
            Player.Play("CarSmiling");
        }
    }
    public void PlayLanded()
    {
        SoundManager.instance.PlayBetween(46,48);
        idle = false;
        Player.Play("CarSurprised");
        Invoke(nameof(SetIdle), 2);
    }
    public void PlaySmile()
    {
        if (idle)
        {
           SoundManager.instance.PlayBetween(33,46);
            idle = false;
           // int smileIndex = Random.Range(1, 4);
          //  Player.Play(smileIndex.ToString(),0);

            Player.Play("CarHappy");
            Invoke(nameof(SetIdle), 1.5f);
        }
    }
    public void PlayConfuse()
    {
        if (idle)
        {
           SoundManager.instance.PlayBetween(48, 53);
            idle = false;
            Player.Play("CarSurprised");
            Invoke(nameof(SetIdle), 1.5f);
        }
    }
    public void PlayBack()
    {
        if (idle)
        {         
            idle = false;
            Player.Play("CarHappyNoBlink");
            Invoke(nameof(SetIdle), 1.5f);
        }
    }
    public void CarBack()
    {
        if (idle)
        {
            idle = false;
            Player.Play("CarHappyNoBlink");
        }
    }
    public void Win()
    {
        // int smileIndex = Random.Range(1, 4);
        // Player.Play(smileIndex.ToString(), 0);
        Player.Play("CarSmiling");
    }

}
