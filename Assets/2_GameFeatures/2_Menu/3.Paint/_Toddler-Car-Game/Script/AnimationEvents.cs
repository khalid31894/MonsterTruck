using UnityEngine;
//using DanielLochner.Assets.SimpleScrollSnap;
using System.Collections;
using System.Collections.Generic;
public class AnimationEvents : MonoBehaviour
{
    public float x;
    public bool Character = false, Loading = false,gameplay = false;
    public GameObject ob;
    
    private void Awake()
    {
        print("invoked");
        if (Character)
        {
            GetComponent<Animator>().Play("idle");
        }
        if (!Character && !Loading && !gameplay)
        {
            x = transform.GetChild(0).GetChild(0).transform.localPosition.x;
        }
        else if (Character)
        {
            Invoke(nameof(PlayAnOther), 2);
        }
        else if (Loading)
        {
            //if (CarController.ReadyForAd)
            {
                //CarController.ReadyForAd = false;
                Invoke(nameof(Ad), 1);
            }
            Invoke(nameof(EndLoading), 2);
        }
        else if (gameplay)
        {
            
            Invoke(nameof(Game_play), 1);
        }
    }
   
    void Game_play()
    {
        Car_Paint.Next_Scene("ModeSelection");
    }

    [System.Obsolete]
    void Ad()
    {

        //if (IntitializeAdmobAds_CB.interstitialHigh == null)
        //    Debug.Log("interstitialHigh is null");

        //AssignAdIds_CB.instance.CallInterstitialAd(Adspref.JustStatic);

       
    }
    public void EndLoading()
    {
        if (ob)
        {
            ob.SetActive(true);
        }
        gameObject.SetActive(false);
    }
    public void EndAnim()
    {
        if (gameObject.activeInHierarchy)
        {
            GetComponent<Animator>().enabled = false;
            //GetComponent<SimpleScrollSnap>().enabled = true;
        }
    }
    public void PlayAnOther()
    {
        int r = Random.Range(1, 5);
        if (gameObject.activeInHierarchy)
        {
            GetComponent<Animator>().Play(r.ToString());
        }
    }
    public void EndIdle()
    {
        Invoke(nameof(PlayAnOther), 2);
    }
    public void PlaySmileSounds()
    {
        //SoundManager.instance.PlayBetween(40, 46);
    }
    public void PlayWooSounds()
    {
        //SoundManager.instance.PlayBetween(49, 53);
    }
    public void PlayjumpSounds()
    {
        //SoundManager.instance.PlayEffect_Instance(55);
    }
    public void PlayjumpLaughSounds()
    {
        //    SoundManager.instance.PlayEffect_Instance(56);
    }
}
