//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using RS = UnityEngine.RemoteSettings;
////using GameAnalyticsSDK;
////using ToastPlugin;

//public class AdsInitilizer : MonoBehaviour
//{
//    public static AdsInitilizer instance;
//    public static bool PersonlizedAds = false;
//    public static bool CanShowInterstitialAds = true;
//    public static bool RemoteValueAds = true;

//    public int deviceMemNeeded;


//    private void Awake()
//    {
//        instance = this;
//        deviceMemNeeded = 1024;
//        CallAdsNow();
//    }
//    public int returnMem()
//    {
//        int mem;
//        mem = SystemInfo.systemMemorySize;
//        return mem;
//    }
//    [Obsolete]
//    public void CallAdsNow()
//    {
//        Debug.Log(returnMem());
//        if (returnMem() <= deviceMemNeeded)
//        {
//            Debug.Log("Ads not initlize");

//            return;
//        }
//        else
//        {
//            InitlizeAdNetworks();
//            PersonlizedAds = false;
//           // StartCoroutine(CallAds());
//        }
       

//        //PersonlizedAds = false;
//       // StartCoroutine(CallAds());
//    }

//   // [Obsolete]
//    //IEnumerator CallAds()
//    //{
//    //    yield return new WaitForSecondsRealtime(1f);
//    //    while (Application.internetReachability == NetworkReachability.NotReachable)
//    //    {
//    //        yield return new WaitForSecondsRealtime(30f);
//    //    }
//    //    InitlizeAdNetworks();
//    //}

//    [Obsolete]
//    void InitlizeAdNetworks()
//    {
//        IntitializeAdmobAds_CB._instance.CallAds();
//    }

//}