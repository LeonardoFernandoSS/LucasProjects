using UnityEngine;
using System.Collections;

public class ADSEventListener : MonoBehaviour {
	/*
	void OnEnable() {

#if UNITY_ANDROID
		AdMobAndroidManager.failedToReceiveAdEvent += failedToReceiveAdEvent;
		AdMobAndroidManager.leavingApplicationEvent += leavingApplicationEvent;
		AdMobAndroidManager.receivedAdEvent += receivedAdEvent;
		AdMobAndroidManager.interstitialDismissingScreenEvent += interstitialDismissingScreenEvent;
		AdMobAndroidManager.interstitialFailedToReceiveAdEvent += interstitialFailedToReceiveAdEvent;
#elif UNITY_IPHONE
		ADBannerView.onBannerWasClicked += leavingApplicationEvent;
		ADBannerView.onBannerWasLoaded  += receivedAdEvent;
		ADInterstitialAd.onInterstitialWasLoaded  += onFullBannerLoaded;
#endif

	}	
	
	void OnDisable() {

#if UNITY_ANDROID
		AdMobAndroidManager.failedToReceiveAdEvent -= failedToReceiveAdEvent;
		AdMobAndroidManager.leavingApplicationEvent -= leavingApplicationEvent;
		AdMobAndroidManager.receivedAdEvent -= receivedAdEvent;		
		AdMobAndroidManager.interstitialDismissingScreenEvent -= interstitialDismissingScreenEvent;
		AdMobAndroidManager.interstitialFailedToReceiveAdEvent -= interstitialFailedToReceiveAdEvent;
#elif UNITY_IPHONE
		ADBannerView.onBannerWasClicked -= leavingApplicationEvent;
		ADBannerView.onBannerWasLoaded  -= receivedAdEvent;
		ADInterstitialAd.onInterstitialWasLoaded  -= onFullBannerLoaded;
#endif

	}
		
#if UNITY_ANDROID
	void failedToReceiveAdEvent(string error){

		StartCoroutine(ADSController.createBanner());

	}
#endif
	
	void leavingApplicationEvent(){

		StartCoroutine(ADSController.refresh());

	}		
	
	void receivedAdEvent(){

		StartCoroutine(ADSController.hideBanner());

	}

#if UNITY_ANDROID
	void interstitialDismissingScreenEvent(){

		ADSController.lastTimeShowedFullBanner = Time.time;

		ADSController.fullBannerLocked = false;

	}	
	
	void interstitialFailedToReceiveAdEvent(string error){

		StartCoroutine(ADSController.createFullBanner());

	}
#endif

#if UNITY_IPHONE
	void onFullBannerLoaded(){
		
		iAd.setFullBannerReady(true);
				
	}
#endif
	*/
}