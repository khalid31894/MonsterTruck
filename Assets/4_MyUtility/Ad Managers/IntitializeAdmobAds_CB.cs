
//using UnityEngine;
//using System;
//using GoogleMobileAds.Api;
//using GoogleMobileAds;
//using System.Collections;
//using UnityEngine.UI;
//using System.Collections.Generic;
////using GameAnalyticsSDK;

//public class IntitializeAdmobAds_CB : MonoBehaviour
//{

//    public static BannerView bannerView;
//    private int[] bannerAdPriorities = { 1, 2, 3 };
//    public static InterstitialAd interstitialHigh;
//    public static InterstitialAd interstitialMid;
//    public static InterstitialAd interstitialLow;
//    public bool IsInitialized = false;
//    public bool PersonlizedAds;
//    public static IntitializeAdmobAds_CB _instance;
//    // Use this for initialization
//    private void Awake()
//    {
//        _instance = this;
//    }
//    private void Start()
//    {
//        IsInitialized = false;
//    }

//    [Obsolete]
//    public void CallAds()
//    {
//        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };
//        deviceIds.Add("2AF741C924B14854891FE16F91D87B40");

//        RequestConfiguration requestConfiguration = new RequestConfiguration.Builder()
//            .SetMaxAdContentRating(MaxAdContentRating.G)
//            .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.True)
//            .SetTestDeviceIds(deviceIds).build();
//        MobileAds.SetRequestConfiguration(requestConfiguration);
//        try
//        {

//            MobileAds.Initialize(initStatus =>
//            {
//                //Debug.Log("Initialized Admobe here ");
//                try
//                {
//                    if (PlayerPrefs.GetInt("RemoveAds") == 0)
//                    {
//                       //// RequestBannerHigh();
//                        RequestAdmobInterstitialHigh();
//                        RequestAdmobInterstitialMedium();
//                        RequestAdmobInterstitialLow();
//                    }
//                   //else if(PlayerPrefs.GetInt("RemoveAds") == 1&& PlayerPrefs.GetInt("RemoteConfigOff") == 0)
//                   // {
//                   //     RequestAdmobInterstitialHigh();
//                   //     RequestAdmobInterstitialMedium();
//                   //     RequestAdmobInterstitialLow();
//                   // }
//                    IsInitialized = true;
//                    IsFromAd = true;
//                }
//                catch (Exception)
//                {
//                    // GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
//                }
//            });
//        }
//        catch (Exception)
//        {
//            //  GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
//        }

//    }
//    public static bool IsFromAd = false;

//    void RequestForFamily()
//    {
//        RequestConfiguration requestConfiguration;
//        if (AssignAdIds_CB.instance.IsForFamily)
//        {
//            requestConfiguration = new RequestConfiguration.Builder()
//            .SetTagForUnderAgeOfConsent(TagForUnderAgeOfConsent.True)
//            .build();

//        }
//        else
//        {
//            requestConfiguration = new RequestConfiguration.Builder()
//            .SetTagForUnderAgeOfConsent(TagForUnderAgeOfConsent.False)
//            .build();

//        }
//        MobileAds.SetRequestConfiguration(requestConfiguration);
//    }

//    [Obsolete]
//    public void RequestAdmobInterstitialHigh()
//    {
//        RequestForFamily();
//        // Initialize an InterstitialAd.
//        string InterId = null;
//        if (AssignAdIds_CB.instance.AdmobTestIds)
//        {
//            print("In test ID");
//            InterId = "ca-app-pub-3940256099942544/1033173712";
//            AssignAdIds_CB.instance.admobInterstial_High = InterId;
//        }
//        else
//        {
//            print("In live ID");
//        }
//        if (interstitialHigh != null) interstitialHigh.Destroy();
//        interstitialHigh = new InterstitialAd(AssignAdIds_CB.instance.admobInterstial_High);

//        // Called when an ad request has successfully loaded.
//        interstitialHigh.OnAdLoaded += HandleOnAdLoadedIntersitalHigh;
//        // Called when an ad request failed to load.
//        interstitialHigh.OnAdFailedToLoad += HandleOnAdFailedToLoadIntersitalHigh;
//        // Called when an ad is shown.
//        interstitialHigh.OnAdOpening += HandleOnAdOpenedIntersitalHigh;
//        // Called when the ad is closed.
//        interstitialHigh.OnAdClosed += HandleOnAdClosedIntersitalHigh;
//        // Create an empty ad request.
//        AdRequest request;

//        if (PersonlizedAds)
//        {
//            request = new AdRequest.Builder().AddExtra("npa", "1").Build();
//        }
//        else
//        {
//            request = new AdRequest.Builder().AddExtra("npa", "0").Build();
//        }
//        interstitialHigh.LoadAd(request);
//    }

//    [Obsolete]
//    public void RequestAdmobInterstitialMedium()
//    {
//            RequestForFamily();
//            string InterId = null;
//            if (AssignAdIds_CB.instance.AdmobTestIds)
//            {
//                InterId = "ca-app-pub-3940256099942544/1033173712";
//                AssignAdIds_CB.instance.admobInterstial_Mid = InterId;
//            }
//            if (interstitialMid != null) interstitialMid.Destroy();
//            // Initialize an InterstitialAd.
//            interstitialMid = new InterstitialAd(AssignAdIds_CB.instance.admobInterstial_Mid);

//            // Called when an ad request has successfully loaded.
//            interstitialMid.OnAdLoaded += HandleOnAdLoadedIntersitalMid;
//            // Called when an ad request failed to load.
//            interstitialMid.OnAdFailedToLoad += HandleOnAdFailedToLoadIntersitalMid;
//            // Called when an ad is shown.
//            interstitialMid.OnAdOpening += HandleOnAdOpenedIntersitalMid;
//            // Called when the ad is closed.
//            interstitialMid.OnAdClosed += HandleOnAdClosedIntersitalMid;
//            // Create an empty ad request.
//            AdRequest request;

//            if (PersonlizedAds)
//            {
//                request = new AdRequest.Builder().AddExtra("npa", "1").Build();
//            }
//            else
//            {
//                request = new AdRequest.Builder().AddExtra("npa", "0").Build();
//            }
//            interstitialMid.LoadAd(request);
//    }

//    [Obsolete]
//    public void RequestAdmobInterstitialLow()
//    {
//        RequestForFamily();
//        string InterId = null;
//        if (AssignAdIds_CB.instance.AdmobTestIds)
//        {
//            InterId = "ca-app-pub-3940256099942544/1033173712";
//            AssignAdIds_CB.instance.admobInterstial_Low = InterId;
//        }
//        if (interstitialLow != null) interstitialLow.Destroy();
//        // Initialize an InterstitialAd.
//        interstitialLow = new InterstitialAd(AssignAdIds_CB.instance.admobInterstial_Low);

//        // Called when an ad request has successfully loaded.
//        interstitialLow.OnAdLoaded += HandleOnAdLoadedIntersitalLow;
//        // Called when an ad request failed to load.
//        interstitialLow.OnAdFailedToLoad += HandleOnAdFailedToLoadIntersitalLow;
//        // Called when an ad is shown.
//        interstitialLow.OnAdOpening += HandleOnAdOpenedIntersitalLow;
//        // Called when the ad is closed.
//        interstitialLow.OnAdClosed += HandleOnAdClosedIntersitalLow;
//        // Create an empty ad request.
//        AdRequest request;

//        if (PersonlizedAds)
//        {
//            request = new AdRequest.Builder().AddExtra("npa", "1").Build();
//        }
//        else
//        {
//            request = new AdRequest.Builder().AddExtra("npa", "0").Build();
//        }
//        interstitialLow.LoadAd(request);
//    }

//    [Obsolete]
//    public void ShowAdmobInterstialAd()
//    {
//        AssignAdIds_CB.instance.play_Ad = false;
//        if (interstitialHigh.IsLoaded())
//        {
//            interstitialHigh.Show();
            
//        }
//        else if (interstitialMid.IsLoaded())
//        {
//            RequestAdmobInterstitialHigh();
//            interstitialMid.Show();
//        }
//        else if (interstitialLow.IsLoaded())
//        {
//            RequestAdmobInterstitialHigh();
//            RequestAdmobInterstitialMedium();
//            interstitialLow.Show();
//        }
        
//    }

//    [Obsolete]
//    public bool HasAdmobInterstialAvaible()
//    {
//        if (!IsInitialized)
//        {
//            return false;
//        }
//        if (IntitializeAdmobAds_CB.interstitialHigh.IsLoaded())
//        {
//            return true;
//        }
//        else if (IntitializeAdmobAds_CB.interstitialMid.IsLoaded())
//        {
//            return true;
//        }
//        else if (IntitializeAdmobAds_CB.interstitialLow.IsLoaded())
//        {
//            return true;
//        }
//        else
//        {
//            RequestAdmobInterstitialHigh();
//            RequestAdmobInterstitialMedium();
//            RequestAdmobInterstitialLow();
//            return false;
//        }
//    }

//    int bannerToLoad = 0;
//    int failed = 0;
//    public void RequestBannerHigh()
//    {
//        if (failed > 5)
//        {
//            return;
//        }
//        if (!IsInitialized)
//        {
//            return;
//        }
//        string bannerId = null;
//        RequestForFamily();
//        if (AssignAdIds_CB.instance.AdmobTestIds)
//        {
//            bannerId = "ca-app-pub-3940256099942544/6300978111";
//            AssignAdIds_CB.instance.bannerAdUnitIds[0] = bannerId;
//            AssignAdIds_CB.instance.bannerAdUnitIds[1] = bannerId;
//            AssignAdIds_CB.instance.bannerAdUnitIds[2] = bannerId;
//        }

//        string adUnitId = AssignAdIds_CB.instance.bannerAdUnitIds[bannerToLoad];
//        int priority = bannerAdPriorities[bannerToLoad];
//        DestoryBanner();
//        if (AssignAdIds_CB.instance.bannerType == BannerType.Simple_Banner)
//        {
//            bannerView = new BannerView(AssignAdIds_CB.instance.bannerAdUnitIds[bannerToLoad], AdSize.Banner, AssignAdIds_CB.instance.adsPosition);
//        }
//        else if (AssignAdIds_CB.instance.bannerType == BannerType.Adaptive_Banner)
//        {
//            AdSize adaptiveSize = AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);
//            bannerView = new BannerView(AssignAdIds_CB.instance.bannerAdUnitIds[bannerToLoad], adaptiveSize, AssignAdIds_CB.instance.adsPosition);
//        }
//        else
//        {
//            bannerView = new BannerView(AssignAdIds_CB.instance.bannerAdUnitIds[bannerToLoad], AdSize.SmartBanner, AssignAdIds_CB.instance.adsPosition);
//        }

//        // Create an ad request with the highest priority
//        AdRequest bannerAdRequest = new AdRequest.Builder()
//            .AddExtra("id", adUnitId)
//            .AddExtra("npa", "1")
//            .AddExtra("priority", priority.ToString())
//            .Build();

//        // Load the banner ad with the ad request
//        bannerView.LoadAd(bannerAdRequest);


//        // Called when an ad request has successfully loaded.
//        bannerView.OnAdLoaded += HandleOnAdLoadedHigh;

//        // Called when an ad request failed to load.
//        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoadHigh;

//        // Called when an ad is clicked.
//        bannerView.OnAdOpening += HandleOnAdOpenedHigh;

//        // Called when the user returned from the app after an ad click.
//        bannerView.OnAdClosed += HandleOnAdClosedHigh;

//    }

//    public void DestoryBanner()
//    {
//        if (bannerView != null) bannerView.Destroy();
//    }
//    public void HideBanner()
//    {
//        bannerView.Hide();

//    }
//    public void ShowBanner()
//    {
//      // try
//       // {
//            bannerView.Show();

//    //    }
//        //catch (Exception ex)
//        //{
//        //   // GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
//        //}

//    }

//    #region Call Back Banner High
//    public void HandleOnAdLoadedHigh(object sender, EventArgs args)
//    {
//        //EventReporting.ReportAdEvent(GAAdAction.Loaded, GAAdType.Banner);
//        //GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.Banner, "admob", "GA");
//        MonoBehaviour.print("HandleAdLoaded event received");
//    }

//    public void HandleOnAdFailedToLoadHigh(object sender, AdFailedToLoadEventArgs args)
//    {
//        failed++;
//        if (failed < 5)
//        {
//            bannerToLoad++;
//            if (bannerToLoad >= AssignAdIds_CB.instance.bannerAdUnitIds.Length)
//            {
//                bannerToLoad = AssignAdIds_CB.instance.bannerAdUnitIds.Length - 1;
//            }
//            RequestBannerHigh();
//        }
//        //GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Banner, "admob", "GA");
//        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
//        + args.LoadAdError.ToString());
//    }

//    public void HandleOnAdOpenedHigh(object sender, EventArgs args)
//    {
//        //GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Banner, "admob", "GA");
//        MonoBehaviour.print("HandleAdOpened event received");
//    }

//    public void HandleOnAdClosedHigh(object sender, EventArgs args)
//    {
//        RequestBannerHigh();
//    }


//    #endregion

//    #region Call Back Banner Mid
//    public void HandleOnAdLoadedMid(object sender, EventArgs args)
//    {
//        //EventReporting.ReportAdEvent(GAAdAction.Loaded, GAAdType.Banner);
//        //GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.Banner, "admob", "GA");
//        MonoBehaviour.print("HandleAdLoaded event received");
//    }

//    public void HandleOnAdFailedToLoadMid(object sender, AdFailedToLoadEventArgs args)
//    {
//        //  GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Banner, "admob", "GA");
//        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
//        + args.LoadAdError.ToString());
//    }

//    public void HandleOnAdOpenedMid(object sender, EventArgs args)
//    {
//        //GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Banner, "admob", "GA");
//        MonoBehaviour.print("HandleAdOpened event received");
//    }

//    public void HandleOnAdClosedMid(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdClosed event received");
//    }


//    #endregion
//    #region Call Back Banner Low
//    public void HandleOnAdLoadedLow(object sender, EventArgs args)
//    {
//        //EventReporting.ReportAdEvent(GAAdAction.Loaded, GAAdType.Banner);
//        // GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.Banner, "admob", "GA");
//        MonoBehaviour.print("HandleAdLoaded event received");
//    }
//    public void HandleOnAdFailedToLoadLow(object sender, AdFailedToLoadEventArgs args)
//    {
//        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
//        + args.LoadAdError.ToString());
//        //GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Banner, "admob", "GA");
//    }
//    public void HandleOnAdOpenedLow(object sender, EventArgs args)
//    {
//        //GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Banner, "admob", "GA");
//        MonoBehaviour.print("HandleAdOpened event received");
//    }
//    public void HandleOnAdClosedLow(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdClosed event received");
//    }
//    #endregion

//    #region Admob Interstial Call Back High
//    public void HandleOnAdLoadedIntersitalHigh(object sender, EventArgs args)
//    {
//        //  GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.Interstitial, "admob", "GA");
//        // AddReporting.instace.ReportAdEvent(AdType.Interstitial, AdAction.Loaded);
//        print("HandleAdLoaded event received");
//    }

//    public void HandleOnAdFailedToLoadIntersitalHigh(object sender, AdFailedToLoadEventArgs args)
//    {
//        // GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Interstitial, "admob", "GA");
//        //ToastHelper.ShowToast("Failed "+ args.Message);
//        print("HandleFailedToReceiveAd event received with message: "
//        + args.LoadAdError.ToString());
//        // GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.InterstitialHigh, "admob", "GA");
//    }

//    public void HandleOnAdOpenedIntersitalHigh(object sender, EventArgs args)
//    {
//        // GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "admob", "GA");
//        print("HandleAdOpened event received");
//    }

//    [Obsolete]
//    public void HandleOnAdClosedIntersitalHigh(object sender, EventArgs args)
//    {

//        //if (InterstitialBasedItemsUnlocking_CB.callBackObjectForInter != null)
//        //{
//        //    InterstitialBasedItemsUnlocking_CB.callBackObjectForInter.SendMessage("AdWatched");
//        //}
//        interstitialHigh.Destroy();
//        RequestAdmobInterstitialHigh();
//        if (!interstitialMid.IsLoaded())
//        {
//            RequestAdmobInterstitialMedium();
//        }
//        if (!interstitialLow.IsLoaded())
//        {
//            RequestAdmobInterstitialLow();
//        }
//        // GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.InterstitialHigh, "admob", "GA");
//        print("HandleAdClosed event received");
//    }
//    #endregion
//    #region Admob Interstial Call Back Mid
//    public void HandleOnAdLoadedIntersitalMid(object sender, EventArgs args)
//    {
//        // AddReporting.instace.ReportAdEvent(AdType.Interstitial, AdAction.Loaded);
//        // GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.Interstitial, "admob", "GA");
//        print("HandleAdLoaded event received");
//    }

//    public void HandleOnAdFailedToLoadIntersitalMid(object sender, AdFailedToLoadEventArgs args)
//    {
//        //ToastHelper.ShowToast("Failed "+ args.Message);
//        print("HandleFailedToReceiveAd event received with message: "
//        + args.LoadAdError.ToString());
//        // GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Interstitial, "admob", "GA");
//    }

//    public void HandleOnAdOpenedIntersitalMid(object sender, EventArgs args)
//    {
//        //  GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "admob", "GA");
//        print("HandleAdOpened event received");
//    }

//    [Obsolete]
//    public void HandleOnAdClosedIntersitalMid(object sender, EventArgs args)
//    {
//        //if (InterstitialBasedItemsUnlocking_CB.callBackObjectForInter != null)
//        //{
//        //    InterstitialBasedItemsUnlocking_CB.callBackObjectForInter.SendMessage("AdWatched");
//        //}
//        interstitialMid.Destroy();
//        RequestAdmobInterstitialMedium();
//        RequestAdmobInterstitialHigh();
//        if (!interstitialLow.IsLoaded())
//        {
//            RequestAdmobInterstitialLow();
//        }
//        // GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.InterstitialMid, "admob", "GA");
//        print("HandleAdClosed event received");
//    }
//    #endregion
//    #region Admob Interstial Call Back Low
//    public void HandleOnAdLoadedIntersitalLow(object sender, EventArgs args)
//    {
//        //  GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.Interstitial, "admob", "GA");
//        // AddReporting.instace.ReportAdEvent(AdType.Interstitial, AdAction.Loaded);
//        print("HandleAdLoaded event received");
//    }
//    public void HandleOnAdFailedToLoadIntersitalLow(object sender, AdFailedToLoadEventArgs args)
//    {
//        //GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Interstitial, "admob", "GA");
//        //ToastHelper.ShowToast("Failed "+ args.Message);
//        print("HandleFailedToReceiveAd event received with message: "
//        + args.LoadAdError.ToString());
//        // GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.InterstitialLow, "admob", "GA");
//    }

//    public void HandleOnAdOpenedIntersitalLow(object sender, EventArgs args)
//    {
//        // GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "admob", "GA");
//        print("HandleAdOpened event received");
//    }

//    [Obsolete]
//    public void HandleOnAdClosedIntersitalLow(object sender, EventArgs args)
//    {
//        //if (InterstitialBasedItemsUnlocking_CB.callBackObjectForInter != null)
//        //{
//        //    InterstitialBasedItemsUnlocking_CB.callBackObjectForInter.SendMessage("AdWatched");
//        //}
//        interstitialLow.Destroy();
//        RequestAdmobInterstitialHigh();
//        RequestAdmobInterstitialMedium();
//        RequestAdmobInterstitialLow();
//        //  GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.InterstitialLow, "admob", "GA");
//        print("HandleAdClosed event received");
//    }
//    #endregion


//}
