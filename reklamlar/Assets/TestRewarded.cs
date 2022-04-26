using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;


public class TestRewarded : MonoBehaviour
{
      public string RewardedIDForAndroid;
    //  public TextMeshProUGUI Status;

    private RewardedAd rewardedAd;

    public Text Status;
    public enum TestRequestType { AdRequest, RequestConfig }
    public TestRequestType requestType = TestRequestType.AdRequest;
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }



    public void RequestRewarded()
    {

#if UNITY_ANDROID
        string adUnitId = RewardedIDForAndroid;

#elif UNITY_ANDROID
        string adUnityId = "ca-app-pub-3940256099942544/6300978111";
#else
        string adUnityId = "unexpected_platform";
#endif


        rewardedAd = new RewardedAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
        Status.text = "Requested Intestitial";
        EnlableDelegates();
    }

    public void ShowRewarded()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
            Status.text = "Rewarded ad Shown";
        }
        //else
        //{
        //    Status.text = "Rewarded ad is not ready yet";
        //}
    }

    public void testRequestRewarded()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/5224354917";

#elif UNITY_ANDROID
        string adUnityId = "ca-app-pub-3940256099942544/5224354917";
#else
        string adUnityId = "unexpected_platform";
#endif
        string PhoneGoogleAdId = "fe60462c35bb43f2a8b3c6afb69070b6";

        switch (requestType)
        {
            case TestRequestType.AdRequest:
                rewardedAd = new RewardedAd(adUnitId);
                //EnableDelegates();
                AdRequest request = CreateAdRequest(PhoneGoogleAdId);
                rewardedAd.LoadAd(request);
                break;
            case TestRequestType.RequestConfig:
                MobileAds.SetRequestConfiguration(CreateRequestConfiguration(PhoneGoogleAdId));
                break;
        }


    }


    private RequestConfiguration CreateRequestConfiguration(string phoneID)
    {
        List<string> deviceIds = new List<string>();
        deviceIds.Add(phoneID);
        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(deviceIds)
            .build();

        return requestConfiguration;
    }


    private AdRequest CreateAdRequest(string phoneID)
    {
        return new AdRequest.Builder()
            .AddTestDevice(AdRequest.TestDeviceSimulator)
            .AddTestDevice(phoneID)
            .AddKeyword("unity-admob-sample")
            .TagForChildDirectedTreatment(false)
            .AddExtra("color_bg", "9B30FF")
            .Build();
    }

    public void testShowRewarded()
    {

        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
        else
        {
            Status.text = "Intersitial ad is not ready yet";
        }


    }



    private void EnlableDelegates()
    {
        rewardedAd.OnAdLoaded += HandleOnAdLoaded;
        rewardedAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        rewardedAd.OnAdOpening += HandleOnAdOpened;
        rewardedAd.OnAdClosed += HandleOnAdClosed;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow;
       
    }


    private void OnDisable()
    {
        rewardedAd.OnAdLoaded -= HandleOnAdLoaded;
        rewardedAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        rewardedAd.OnAdOpening -= HandleOnAdOpened;
        rewardedAd.OnAdClosed -= HandleOnAdClosed;
        rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        rewardedAd.OnAdFailedToShow -= HandleRewardedAdFailedToShow;
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Status.text = "Rewarded HandleAdLoaded event recieved";
    }
    public void HandleOnAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        Status.text = "Rewarded HandleFailedToRecieveAd event recieved with message: " + args.Message;
    }
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        Status.text = "Rewarded HandleAdOpened event recieved";
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Status.text = "Rewarded HandleAdClosed event recieved";
    }
    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        Status.text = "Rewarded HandleAdFailedToShow event recieved with message: " + args.Message; ;
    }
    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;

        Status.text = "Rewarded HandleUserEarnedReward event recieved for: "+ amount.ToString()+ " "+ type;
    }


}
