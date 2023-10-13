//using GameAnalyticsSDK;
using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.Purchasing;

public class AssignAdIds_CB : MonoBehaviour
{
    public bool TestingInEditor = false;
    public bool IsForFamily = false;
    public bool AdmobTestIds = false;
    //public static int adsCounter = 0;
    public static AssignAdIds_CB instance;
    public AdsCaller_CB adCaller;
    public string admobAppID;
    [Header("Banner Ad")]
    public string[] bannerAdUnitIds;
    [Header("Interstitial Ad")]
    public string admobInterstial_High;
    public string admobInterstial_Mid;
    public string admobInterstial_Low;
    public AdPosition adsPosition;
    public BannerType bannerType = BannerType.Simple_Banner;
    public ShowBannerEnum showBanner = ShowBannerEnum.showBanner;
    public bool wantToSkipFirstAd = false;
    [Header("Unity InApp Define")]
    public InAppKeys[] InAppIds;
    public static InAppKeys[] keys;
    public GameObject inAppGameObject;
    public static GameObject inappObj;
    [HideInInspector]
    public unityInAppPurchase_CB IAP;


    void Awake()
    {
        try
        {
            instance = this;
            play_Ad = false;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DontDestroyOnLoad(gameObject);
            showBanner = (ShowBannerEnum)PlayerPrefs.GetInt("show_banner", (int)showBanner);
            bannerType = (BannerType)PlayerPrefs.GetInt("banner_type", (int)bannerType);
            SendRetentionAnalytics();

#if UNITY_IOS
            rates_us=ratesusIOS;
#endif

#if UNITY_ANDROID || UNITY_EDITOR
            // rates_us = rates_us = "https://play.google.com/store/apps/details?id=" + Application.identifier;
#endif
            keys = InAppIds;
            inappObj = inAppGameObject;
            IAP = inappObj.GetComponent<unityInAppPurchase_CB>();
            StartCoroutine(CallForUpdateAdIds());
        }
        catch (Exception ex)
        {
            // GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
        }

    }

    DateTime oldDate;
    DateTime currentDate;
    DateTime lastNotify;
    public void SendRetentionAnalytics()
    {
        if (!PlayerPrefs.HasKey("StartDate"))
        {
            PlayerPrefs.SetString("StartDate", DateTime.Now.ToBinary().ToString());
        }
        long temp = Convert.ToInt64(PlayerPrefs.GetString("StartDate"));

        //Convert the old time from binary to a DataTime variable
        oldDate = DateTime.FromBinary(temp);
        //Debug.Log("oldDate: " + oldDate);

        currentDate = System.DateTime.Now;
        //Debug.Log("Date : " + currentDate);

        //Use the Subtract method and store the result as a timespan variable
        TimeSpan difference = currentDate.Subtract(oldDate);
        //Debug.Log("Difference: " + difference + " : Days : " + difference.Days);
        SendAnalytics("Self Retention", "AssignAdIds", difference.TotalDays.ToString());


    }

    public void SendAnalytics(string eventName, string callingFrom, string anyValue)
    {
        Analytics.CustomEvent(eventName, new Dictionary<string, object>
        {
            { callingFrom, anyValue },
        });
    }

    [Obsolete]
    public void ShowMyBanner()
    {
        if (PlayerPrefs.GetInt("RemoveAds", 0) == 0)
        {
            IntitializeAdmobAds_CB._instance.RequestBannerHigh();
        }
    }

    [Obsolete]
    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("RemoveAds", 0) == 0)
        {
            //GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.Banner, "admob", "GA");
            if (IntitializeAdmobAds_CB.bannerView != null)
            {
                if (showBanner == ShowBannerEnum.showBanner)
                {
                    IntitializeAdmobAds_CB.bannerView.Show();
                }
                else
                {
                    IntitializeAdmobAds_CB.bannerView.Destroy();
                }
            }
            else
            {
                IntitializeAdmobAds_CB._instance.RequestBannerHigh();
            }
        }

    }

    public void HideBanner()
    {
        if (IntitializeAdmobAds_CB.bannerView != null)
        {
            if (showBanner == ShowBannerEnum.showBanner)
            {
                IntitializeAdmobAds_CB.bannerView.Hide();
            }
            else
            {
                IntitializeAdmobAds_CB.bannerView.Destroy();
            }
        }
    }

    public void DestoryBanner()
    {
        if (IntitializeAdmobAds_CB.bannerView != null)
        {
            IntitializeAdmobAds_CB.bannerView.Destroy();
        }
    }

    [Obsolete]
    public void RequestBanner()
    {
        if (PlayerPrefs.GetInt("RemoveAds", 0) == 0)
        {

            if (showBanner == ShowBannerEnum.showBanner)
            {
                if (IntitializeAdmobAds_CB.bannerView != null)
                {
                    if (showBanner == ShowBannerEnum.showBanner)
                    {
                        IntitializeAdmobAds_CB.bannerView.Show();
                    }
                    else
                    {
                        IntitializeAdmobAds_CB._instance.RequestBannerHigh();
                    }
                }
                else
                {
                    IntitializeAdmobAds_CB._instance.RequestBannerHigh();
                }
            }
        }
    }
    public bool play_Ad;
    [Obsolete]
    public void CallInterstitialAd(Adspref adspref, bool oneToOne = false)
    {
        if (play_Ad && (IntitializeAdmobAds_CB.interstitialHigh != null || IntitializeAdmobAds_CB.interstitialMid != null
            || IntitializeAdmobAds_CB.interstitialLow != null))
        {
            //GameAnalytics.NewAdEvent(GAAdAction.Clicked, GAAdType.RewardedVideo, "admob", "GA");
            adCaller.CallingAdsThroughAssignIds(adspref, oneToOne);
            play_Ad = false;
            gameObject.GetComponent<ScreenTouch_Ads>().get_touch = 0;
            gameObject.GetComponent<ScreenTouch_Ads>().IsPlay_Count = true;
        }
        else
        {
            IntitializeAdmobAds_CB._instance.RequestAdmobInterstitialHigh();
            IntitializeAdmobAds_CB._instance.RequestAdmobInterstitialMedium();
            IntitializeAdmobAds_CB._instance.RequestAdmobInterstitialLow();
        }
    }



    IEnumerator CallForUpdateAdIds()
    {
        yield return new WaitForSecondsRealtime(3f);
        // UpdateAdIds();
    }
    #region Call Back for In-Apps
    public static void AfterPurchased(int value)
    {
        try
        {
            if (keys[value].purchaseType == PurchaseType.Remove_Ads)
            {
                print("remove Ads");
                PlayerPrefs.SetInt("RemoveAds", 1);
                GC.Collect();
                Resources.UnloadUnusedAssets();
                SceneManager.LoadScene(unityInAppPurchase_CB.levelName);
            }

            else if (keys[value].purchaseType == PurchaseType.Unlock_All)
            {
                print("UnlockALL Ads");
                PlayerPrefs.SetInt("RemoveAds", 1);
                PlayerPrefs.SetInt("unlockall", 1);
                PlayerPrefs.SetInt("8Hour", 1);
                PlayerPrefs.SetInt("24Hour", 1);
                PlayerPrefs.SetInt("72Hour", 1);
                PlayerPrefs.SetInt("120Hour", 1);
                GC.Collect();
                Resources.UnloadUnusedAssets();
                SceneManager.LoadScene(unityInAppPurchase_CB.levelName);
            }

            else if (keys[value].purchaseType == PurchaseType.Scene_Unlock)
            {
                GC.Collect();
                Resources.UnloadUnusedAssets();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (keys[value].purchaseType == PurchaseType.BundleUnlock)
            {
                GC.Collect();
                Resources.UnloadUnusedAssets();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
            else if (keys[value].purchaseType == PurchaseType.Add_Coins)
            {
                Debug.Log("In-App for coins will add here, Add your coins. After Add coins remove this text.");
            }
        }
        catch (Exception ex)
        {
            //GeneralScript._instance.SendExceptionEmail(ex.Message.ToString());
        }
    }
    #endregion

}
[System.Serializable]
public class InAppKeys
{
    public string Id;
    public ProductType productType;
    public PurchaseType purchaseType = PurchaseType.Remove_Ads;
    public string priceInDollars;
    //public string localizedPrice;

    //[DrawIf("purchaseType", PurchaseType.Scene_Unlock)]
    public string levelObjName;
   // [DrawIf("purchaseType", PurchaseType.Add_Coins)]
    public int NumberOfCoins;
   //    [DrawIf("purchaseType", PurchaseType.BundleUnlock)]
    public string[] objectsNamesList;


    public InAppKeys(string idOfInApp, ProductType productType, PurchaseType purchaseType, string priceInDollar)
    {
        Id = idOfInApp;
        this.productType = productType;
        this.purchaseType = purchaseType;
        this.priceInDollars = priceInDollar;
    }

}


public enum BannerType
{
    Simple_Banner = 1,
    Smart_Banner = 2,
    Adaptive_Banner = 3
}

public enum PurchaseType
{
    Remove_Ads,
    Unlock_All,
    Add_Coins,
    Scene_Unlock,
    BundleUnlock,
}

public enum ShowBannerEnum
{
    hideBanner = 0,
    showBanner = 1,
}

