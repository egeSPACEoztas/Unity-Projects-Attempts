using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;


public class GoogleInterstitialAd : MonoBehaviour
{
    public string InterstitialIdForAndroid;
    //  public TextMeshProUGUI Status;

    private InterstitialAd InterstitialAd;
    public Text Status;
    public enum TestRequestType { AdRequest, RequestConfig }
    public TestRequestType requestType = TestRequestType.AdRequest;
    // Start is called before the first frame update
    void Start()
    {
        MobileAds.Initialize(initStatus => { });
    }

    // Update is called once per frame
    void Update()
    {

    }







    public void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = InterstitialIdForAndroid;

#elif UNITY_ANDROID
        string adUnityId = "ca-app-pub-3940256099942544/6300978111";
#else
        string adUnityId = "unexpected_platform";
#endif


        InterstitialAd = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        InterstitialAd.LoadAd(request);
        Status.text = "Requested Intestitial";
        EnlableDelegates();
    }


    public void ShowInterstitial()
    {
        if (InterstitialAd.IsLoaded())
        {
            InterstitialAd.Show();
            Status.text = "Interstitial ad Shown";
        }
        //else
        //{
        //    Status.text = "Interstitial ad is not ready yet";
        //}
    }


    public void TestRequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/1033173712";

#elif UNITY_ANDROID
        string adUnityId = "ca-app-pub-3940256099942544/1033173712";
#else
        string adUnityId = "unexpected_platform";
#endif
        string PhoneGoogleAdId = "fe60462c35bb43f2a8b3c6afb69070b6";

        switch (requestType)
        {
            case TestRequestType.AdRequest:
                InterstitialAd = new InterstitialAd(adUnitId);
                //EnableDelegates();
                AdRequest request = CreateAdRequest(PhoneGoogleAdId);
                InterstitialAd.LoadAd(request);
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

    public void TestShowInterstetial()
    {
        if (InterstitialAd.IsLoaded())
        {
            InterstitialAd.Show();
        }
        else
        {
            Status.text = "Intersitial ad is not ready yet";
        }

    }


    private void EnlableDelegates()
    {
        InterstitialAd.OnAdLoaded += HandleOnAdLoaded;
        InterstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        InterstitialAd.OnAdOpening += HandleOnAdOpened;
        InterstitialAd.OnAdClosed += HandleOnAdClosed;
        InterstitialAd.OnAdLeavingApplication += HandleOnAdLeavingApplications;

    }

    private void OnDisable()
    {
        InterstitialAd.OnAdLoaded -= HandleOnAdLoaded;
        InterstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        InterstitialAd.OnAdOpening -= HandleOnAdOpened;
        InterstitialAd.OnAdClosed -= HandleOnAdClosed;
        InterstitialAd.OnAdLeavingApplication -= HandleOnAdLeavingApplications;
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Status.text = "Interstitial HandleAdLoaded event recieved";
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Status.text = "Interstitial HandleFailedToRecieveAd event recieved with message: " + args.Message;
    }
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        Status.text = "Interstitial HandleAdOpened event recieved";
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Status.text = "Interstitial HandleAdClosed event recieved";
    }
    public void HandleOnAdLeavingApplications(object sender, EventArgs args)
    {
        Status.text = "Interstitial HandleAdLeavingApplications event recieved";
    }


}