using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class AnimationController : MonoBehaviour
{
   public Animator animator_CAT;
   public Animator animator_DOG;
   public Animator animator_HORSE;

    //public float Speech_Start_Delay;
    //public float Speech_1_Delay;
    //public float Speech_2_Delay;
    //public float Speech_3_Delay;
    //public float Speech_4_Delay;
    //public float Speech_5_Delay;
    //public float Speech_6_Delay;
    //public float Speech_7_Delay;
    //public float Speech_8_Delay;

    //string currentState;

    //CAT Const
    const string Cat_Idle = "Idle";
    const string Cat_IdleBlink = "IdleBlink";
    const string Cat_IdleVoice = "IdleVoice";
    const string Cat_Happy = "Happy";
    const string Cat_Jump = "Jump";
    const string Cat_EN_L7_2 = "EN-L7-2";
    const string Cat_EN_L7_3 = "EN-L7-3";
    const string Cat_EN_L7_4 = "EN-L7-4";
    const string Cat_EN_L7_5 = "EN-L7-5";
    const string Cat_EN_L7_6 = "EN-L7-6";
    const string Cat_EN_L7_7 = "EN-L7-7";
    const string Cat_EN_L7_11 = "EN-L7-11";
    const string Cat_EN_L7_19 = "EN-L7-19";

    //DOG Const
    const string Dog_Idle = "Idle";
    const string Dog_IdleBlink = "IdleBlink";
    const string Dog_IdleVoice = "IdleVoice";
    const string Dog_Happy = "Happy";
    const string Dog_Jump = "Jump";
    const string Dog_EN_L7_2 = "EN-L7-2";
    const string Dog_EN_L7_3 = "EN-L7-3";
    const string Dog_EN_L7_4 = "EN-L7-4";
    const string Dog_EN_L7_5 = "EN-L7-5";
    const string Dog_EN_L7_6 = "EN-L7-6";
    const string Dog_EN_L7_7 = "EN-L7-7";
    const string Dog_EN_L7_11 = "EN-L7-11";
    const string Dog_EN_L7_19 = "EN-L7-19";

    //HORSE Const
    const string Horse_Idle = "Idle";
    const string Horse_IdleBlink = "IdleBlink";
    const string Horse_IdleVoice = "IdleVoice";
    const string Horse_Happy = "Happy";
    const string Horse_Jump = "Jump";
    const string Horse_EN_L7_2 = "EN-L7-2";
    const string Horse_EN_L7_3 = "EN-L7-3";
    const string Horse_EN_L7_4 = "EN-L7-4";
    const string Horse_EN_L7_5 = "EN-L7-5";
    const string Horse_EN_L7_6 = "EN-L7-6";
    const string Horse_EN_L7_7 = "EN-L7-7";
    const string Horse_EN_L7_11 = "EN-L7-11";
    const string Horse_EN_L7_19 = "EN-L7-19";

  
    private void OnEnable()
    {
        StartCoroutine(Char_Cor());
    }
    
    IEnumerator Char_Cor()
    {
        if (PlayerPrefsManager.GetCurrentChar() == 2)
        {
            animator_CAT.gameObject.SetActive(true);

            yield return new WaitForSeconds(2);
            animator_CAT.Play(Cat_EN_L7_2);
            yield return new WaitForSeconds(4);
            animator_CAT.Play(Cat_EN_L7_3);
            yield return new WaitForSeconds(6);
            animator_CAT.Play(Cat_EN_L7_4);
            yield return new WaitForSeconds(6);
            animator_CAT.Play(Cat_EN_L7_5);
        }
        if (PlayerPrefsManager.GetCurrentChar() == 1)
        {
            animator_DOG.gameObject.SetActive(true);

            yield return new WaitForSeconds(2);
            animator_DOG.Play(Cat_EN_L7_2);
            yield return new WaitForSeconds(4);
            animator_DOG.Play(Cat_EN_L7_3);
            yield return new WaitForSeconds(6);
            animator_DOG.Play(Cat_EN_L7_4);
            yield return new WaitForSeconds(6);
            animator_DOG.Play(Cat_EN_L7_5);
        }
        if (PlayerPrefsManager.GetCurrentChar() == 3)
        {
            animator_HORSE.gameObject.SetActive(true);

            yield return new WaitForSeconds(2);
            animator_HORSE.Play(Cat_EN_L7_2);
            yield return new WaitForSeconds(4);
            animator_HORSE.Play(Cat_EN_L7_3);
            yield return new WaitForSeconds(6);
            animator_HORSE.Play(Cat_EN_L7_4);
            yield return new WaitForSeconds(6);
            animator_HORSE.Play(Cat_EN_L7_5);
        }
    }



    private void OnDisable()
    {
        animator_DOG.gameObject.SetActive(false);
        animator_CAT.gameObject.SetActive(false);
        animator_HORSE.gameObject.SetActive(false);
    }
}
