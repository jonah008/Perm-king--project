using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
	private string playStoreID = "4372387";
    private string appStoreID = "4372386";
	
	private string interstitialAd = "Interstitial_Android";
	private string rewardedVideoAd = "Rewarded_Android";
	
	public bool isTargetPlayStore;
	public bool isTestAd;
	
    private void Start()
    {
		Advertisement.AddListener(this);
		InitializeAdvertisement();
        
    }
	private void InitializeAdvertisement()
	{
		if(isTargetPlayStore)
		{
			Advertisement.Initialize(playStoreID, isTestAd);
			return;
		}
		Advertisement.Initialize(appStoreID, isTestAd);
		
	}
	public void PlayInterstitialAd()
	{
		if(!Advertisement.IsReady(interstitialAd))
		{
			return;
		}
		Advertisement.Show(interstitialAd);
	}
	public void PlayRewardedVideo()
	{
		if(!Advertisement.IsReady(rewardedVideoAd))
		{
			return;
		}
		Advertisement.Show(rewardedVideoAd);

	}
	public void OnUnityAdsReady(string placementId){
		//throw new System.NotImplementedException();
		
	}
	public void OnUnityAdsDidError(string message){
		//throw new System.NotImplementedException();
		
	}
	public void OnUnityAdsDidStart(string placementId){
		//throw new System.NotImplementedException();
		
	}
	public void OnUnityAdsDidFinish(string placementId, ShowResult showResult){
		if(showResult == ShowResult.Failed)
		{
			if(placementId == rewardedVideoAd)
			{
				//Debug.Log("reward failed");
			}
			
		}
		else if(showResult == ShowResult.Skipped)
		{
			if(placementId == rewardedVideoAd)
			{
				//Debug.Log("No reward");
			}
			
		}

		else if(showResult == ShowResult.Finished)
		{
			if(placementId == rewardedVideoAd)
			{
				GameManager.instance.RewardPoint();
			}
		}
			
			
		
		
		
		
		
	}
		
	

    
}
