//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
////using GameAnalyticsSDK;
//using RS = UnityEngine.RemoteSettings;
//public enum Adspref { Menu, Selection, GameEnd, GamePause, JustStatic, Ingame, };
//public class AdsCaller_CB : MonoBehaviour
//{
//    public Adspref AdsType;
//    public bool isSplash = false;

//    //public bool oneToOne=false;
//    [Obsolete]
//    private void OnEnable()
//    {
//        try
//        {
//            if (!isSplash)
//            {
//                if (PlayerPrefs.GetInt("RemoveAds", 0) == 0)
//                {
//                    if (!AssignAdIds_CB.instance.wantToSkipFirstAd)
//                    {
//                        if (Application.internetReachability != NetworkReachability.NotReachable)
//                        {
//                            CheckInternetConnectivity_CB._instance.CheckInternetConnection(InternetResponce);
//                        }
//                    }
//                }
//            }
//        }
//        catch (Exception ex)
//        {
//           // GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
//        }
//    }

//    [Obsolete]
//    public void CallingAdsThroughAssignIds(Adspref adspref, bool tempOneToOne = false)
//    {
//        if (PlayerPrefs.GetInt("RemoveAds", 0) == 0)
//        {
//            AdsType = adspref;
//            if (Application.internetReachability != NetworkReachability.NotReachable)
//            {
//                //CheckInternetConnectivity_CB._instance.CheckInternetConnection(InternetResponce);
//                IntitializeAdmobAds_CB._instance.ShowAdmobInterstialAd();
//     //           Debug.Log($"internetReachability:{Application.internetReachability}\nNotReachable:{NetworkReachability.NotReachable} " );
//            }
//        }
//        //else if (PlayerPrefs.GetInt("RemoveAds") == 1 && PlayerPrefs.GetInt("AdsConfig") ==1)
//        //{
//        //    if (Application.internetReachability != NetworkReachability.NotReachable)
//        //    {
//        //        //CheckInternetConnectivity_CB._instance.CheckInternetConnection(InternetResponce);
//        //        IntitializeAdmobAds_CB._instance.ShowAdmobInterstialAd();
//        //        //           Debug.Log($"internetReachability:{Application.internetReachability}\nNotReachable:{NetworkReachability.NotReachable} " );
//        //    }
//        //}
//    }

//    [Obsolete]
//    void InternetResponce(bool resp, Ping p)
//    {

//        if (AssignAdIds_CB.instance.IsForFamily)
//        {
//            if (RS.GetBool("showAds", false) == true)
//            {
//                ApplovinAd();
//            }
//            else
//            {
//                if (IntitializeAdmobAds_CB._instance.HasAdmobInterstialAvaible())
//                {
//                    IntitializeAdmobAds_CB._instance.ShowAdmobInterstialAd();
//                }
//            }
//        }
//        else
//        {
//            ApplovinAd();
//        }
//        //EventReporting.ReportAdEvent(GAAdAction.Request, GAAdType.Interstitial);
//    }

//    [Obsolete]
//    void ApplovinAd()
//    {
//        try
//        {
//            //EventReporting.MaxOrAdmob(GAAdType.Interstitial, "AdmobRequested");
//            //if (IntiizeLovin_CB.instance.HasInterstitalAd())
//            //{
//            //    IntiizeLovin_CB.instance.ShowInterstitialAd();
//            //    EventReporting.MaxOrAdmob(GAAdType.Interstitial, "IronSourceShow");
//            //}
//            //else
//            {
//                //EventReporting.MaxOrAdmob(GAAdType.Interstitial, "IronSourceFailed");
//                if (IntitializeAdmobAds_CB._instance.HasAdmobInterstialAvaible())
//                {
//                    IntitializeAdmobAds_CB._instance.ShowAdmobInterstialAd();
//                    //EventReporting.MaxOrAdmob(GAAdType.Interstitial, "AdmobShow");
//                }
//                else
//                {
//                    //EventReporting.MaxOrAdmob(GAAdType.Interstitial, "AdmobFailed");
//                }

//            }
//        }
//        catch (Exception ex)
//        {
//            //GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
//        }
//    }


//}


