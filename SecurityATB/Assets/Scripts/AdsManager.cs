using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string androidUnitID = "Rewarded_Android";
    [SerializeField] string androidID = "5620327";
    public Button adButton;

    private moneyCounter money;

    private void Awake()
    {
        if (!Advertisement.isInitialized && Advertisement.isSupported) Advertisement.Initialize(androidID, true, this);
        money = GameObject.FindGameObjectWithTag("moneyCounter").GetComponent<moneyCounter>();
    }

    private void Start()
    {
        adButton.interactable = false;
        LoadAd();
    }

    public void LoadAd()
    {
        //Debug.Log("Ad Loading");
        Advertisement.Load(androidUnitID, this);
        OnUnityAdsAdLoaded("Rewarded_Android");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        //if (placementId.Equals(androidID))
        adButton.interactable = true;
        //Debug.Log("Ad Loaded");       
    }

    public void ShowAd()
    {
        Advertisement.Show(androidUnitID, this);
        adButton.interactable = false;
    }

    public void OnInitializationComplete()
    {
        //Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("Load failture" + error.ToString() + message);
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("Show failture" + error.ToString() + message);
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if(placementId.Equals(androidUnitID) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            //Debug.Log("Ad showed");
            money.numberUAH += 50;
            PlayerPrefs.SetInt("money", money.numberUAH);
            LoadAd();
        }
    }
}
