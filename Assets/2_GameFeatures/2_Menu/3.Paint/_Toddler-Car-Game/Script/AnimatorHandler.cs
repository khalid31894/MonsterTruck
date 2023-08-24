using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHandler : MonoBehaviour
{
    public static AnimatorHandler Instance;
    public Animator Player;
    private void OnEnable()
    {
        Instance = this;
        Player = GetComponentInChildren<Animator>();
        //var ch = Player.transform.GetChild(0).GetChild(1).GetChild(0).gameObject;
        //ch.gameObject.AddComponent<BoxCollider2D>().isTrigger = true;
        //ch.gameObject.AddComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Kinematic;
        //ch.AddComponent<CharacterFace>();

    }
    public void SetIdle()
    {
        if (!idle)
        {
            idle = true;
            print("Idle");
            Player.Play("idle");
        }
    }
    public void PlayLanded()
    {
        //SoundManager.instance.PlayBetween(46,48);
        idle = false;
        Player.Play("Landed");
        Invoke(nameof(SetIdle), 2);
    }
    public bool idle = true;
    public void PlaySmile()
    {
        if (idle)
        {
           // SoundManager.instance.PlayBetween(33,46);
            print("smile");         
            idle = false;
            int smileIndex = Random.Range(1, 4);
            Player.Play(smileIndex.ToString(),0);
            //AnimatorStateInfo stateInfo = Player.GetCurrentAnimatorStateInfo(0);
            //float clipTime = stateInfo.normalizedTime * stateInfo.length;
            Invoke(nameof(SetIdle), 1.5f);
        }
    }
    public void PlayConfuse()
    {
        if (idle)
        {
           // SoundManager.instance.PlayBetween(48, 53);
            idle = false;
            Player.Play("Confuse");
            Invoke(nameof(SetIdle), 1.5f);
        }
    }
    public void PlayBack()
    {
        if (idle)
        {         
            idle = false;
            Player.Play("CarBack");
            Invoke(nameof(SetIdle), 1.5f);
        }
    }
    public void CarBack()
    {
        if (idle)
        {
            idle = false;
            Player.Play("CarBack");
        }
    }
    public void Win()
    {
        int smileIndex = Random.Range(1, 4);
        Player.Play(smileIndex.ToString(), 0);
    }

}
