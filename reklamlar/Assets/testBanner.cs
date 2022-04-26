using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using UnityEngine.UI;
using System;


public class testBanner : MonoBehaviour
{
    public string BannerIDForAndroid;
    public Text Status;

    private BannerView bannerView;

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


    public void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = BannerIDForAndroid;

#elif UNITY_ANDROID
        string adUnityId = "ca-app-pub-3940256099942544/6300978111";
#else
        string adUnityId = "unexpected_platform";
#endif


        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
        Status.text = "Requested Banner";
        EnlableDelegates();
    }


    public void ShowBanner()
    {
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
        Status.text = "Show Banner";
    }


    private void EnlableDelegates()
    {
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        bannerView.OnAdOpening += HandleOnAdOpened;
        bannerView.OnAdClosed += HandleOnAdClosed;
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplications;
           
    }

    private void OnDisable()
    {
        bannerView.OnAdLoaded -= HandleOnAdLoaded;
        bannerView.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        bannerView.OnAdOpening -= HandleOnAdOpened;
        bannerView.OnAdClosed -= HandleOnAdClosed;
        bannerView.OnAdLeavingApplication -= HandleOnAdLeavingApplications;
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Status.text = "Banner HandleAdLoaded event recieved";
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
        Status.text = "Banner HandleFailedToRecieveAd event recieved with message: " + args.Message;
    }
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        Status.text = "Banner HandleAdOpened event recieved";
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Status.text = "Banner HandleAdClosed event recieved";
    }
    public void HandleOnAdLeavingApplications(object sender, EventArgs args)
    {
        Status.text = "Banner HandleAdLeavingApplications event recieved";
    }


    //testfunk

    public void TestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-3940256099942544/6300978111";

#elif UNITY_ANDROID
        string adUnityId = "ca-app-pub-3940256099942544/6300978111";
#else
        string adUnityId = "unexpected_platform";
#endif
        string PhoneGoogleAdId = "fe60462c35bb43f2a8b3c6afb69070b6";

        switch (requestType)
        {
            case TestRequestType.AdRequest:
                bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);
                //EnableDelegates();
                AdRequest request = CreateAdRequest(PhoneGoogleAdId);
                bannerView.LoadAd(request);
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
}
