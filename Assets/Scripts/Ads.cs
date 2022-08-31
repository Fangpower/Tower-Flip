using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

[RequireComponent(typeof(Collider2D))]
public class Ads : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public interface IUnityAdsInitializationListener{
        void OnInitializationComplete();
        void OnInitializationFailed(UnityAdsInitializationError error, string message);
    }

    public interface IUnityAdsLoadListener{
        void OnUnityAdsAdLoaded(string placementId);
        void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message);
    }

    public interface IUnityAdsShowListener{
        void OnUnityAdsShowClick(string placementId);
        void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState);
        void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message);
        void OnUnityAdsShowStart(string placementId);
    }

    [Header("Ads properties")]
    [SerializeField] string androidGameId;
    [SerializeField] bool testMode = true;
    //[SerializeField] bool enablePerPlacementMode = true;
    [SerializeField] string rewardedVideoID = "Rewarded_Android";
    [SerializeField] Button rewardButton;

    void Start(){
        InitializeAdsSDK();
        //LoadAd();
    }

    void InitializeAdsSDK(){
        print("Ads Initializing");
        Advertisement.Initialize(androidGameId, testMode, this);
    }

    public void OnInitializationComplete(){
        Debug.Log("Ads Initialized");
        LoadAd();
    }

    private void LoadAd(){
        rewardButton.interactable = true;
        Debug.Log("Loading Ad: " + rewardedVideoID);
        Advertisement.Load(rewardedVideoID, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message){
        Debug.Log("Unity ads failed to initialize. " + error + ": " + message);
    }

    public void OnUnityAdsAdLoaded(string placementId){
        Debug.Log("Unity Ad loaded correctly.");

        if(placementId.Equals(rewardedVideoID)){
            rewardButton.interactable = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message){
        Debug.Log("Unity ads failed to initialize. " + error + ": " + message);
    }

    public void ShowAd(){
        rewardButton.interactable = false;
        Advertisement.Show(rewardedVideoID, this);
    }

    public void OnUnityAdsShowStart(string placementId){}

    public void OnUnityAdsShowClick(string placementId){}

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState){
        if(placementId.Equals(rewardedVideoID) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED)){
            Debug.Log("Ad was finished, reward given");
            FindObjectOfType<LoadingScreen>().Close();
            Advertisement.Load(rewardedVideoID, this);
        }
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message){
        Debug.Log("Ad wasn't shown, reward wasn't given. " + error + ": " + message);
        Advertisement.Load(rewardedVideoID, this);
    }
}
