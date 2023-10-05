using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool isFirstUpdate = true;
    public static bool showMeAd=false;
    private void Update()
    {
        if (isFirstUpdate)
        {
            isFirstUpdate = false;
           // SceneLoader.LoaderCallBack();
           StartCoroutine(waitTime());
        }


    }

   IEnumerator waitTime()
    {
        if (!showMeAd)
        {
            yield return new WaitForSeconds(3f);
            SceneLoader.LoaderCallBack();
        }
        
        if(showMeAd)
        {
            yield return new WaitForSeconds(1.5f);
            if (IntitializeAdmobAds_CB._instance)
            {
                IntitializeAdmobAds_CB._instance.ShowAdmobInterstialAd();
            }
            yield return new WaitForSeconds(1.5f);
            showMeAd = false;
            SceneLoader.LoaderCallBack();
        }

    }
}
