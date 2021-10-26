using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;
	public GameObject[] pans;
	public Button[] buttons;
	public Button insertBut, drawBut, delBut;
	public Button algoA, algoB;
	public GameObject[] balls;
	public GameObject platforms;
	public GameObject bulletPrefab;
	public Transform[] spawnPos;
	public Image reloadImage1, reloadImage2;
	private float reloadAmout1, reloadAmout2;
	public float interval1, interval2;
	public float speed;
	public int spawnIndex;
	public int sortIndex;
	private bool isReloading1;
	private bool isReloading2;
	public Text pointText;
	public Text reloadOneText;
	public GameObject permLogo;
	public GameObject bulletPrefab2;
	public Transform bulletPos;
	public Transform bulletPos2;
	public int rewardCoins;
	public int dataPoints;
	public SpawnerA mySpawnerA;
	public Transform bulletPosition;
	public GameObject[] myBallPrefabs;
	public float sortDuration;
	public Transform[] sortPoint;
	public bool[] ballTypeExist;
	public Transform spawnerPos;
	public Transform positionA;
	public Transform positionB;
	private GameData saveData = new GameData();
	
	
	void Awake(){
		instance = this;
		
	}
    void Start()
    {
		spawnerPos.transform.position = positionA.position;
		
		algoB.gameObject.SetActive(false);
		
		foreach(GameObject pan in pans)
		{
			pan.SetActive(false);
		}
		
        
    }

    
    void Update()
    {
		pointText.text = "POINT:" + saveData.points.ToString();
		if(isReloading1)
		{
			ReloadOne();
		}
		if(isReloading2)
		{
			ReloadTwo();
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			pans[7].SetActive(true);
		}
		CheckTotalNumbers();
		
        
    }
	
	
	public void RemoveLogo()
	{
		permLogo.SetActive(false);
		pans[0].SetActive(true);
		saveData = SaveSystems.controls.LoadGame();
	}
	//insert buttons function;
	public void ShowButtonsPan()
	{
		pans[0].SetActive(false);
		pans[1].SetActive(true);
	}
	public void ShowBalls(int val)
	{
		Instantiate(balls[val], spawnPos[spawnIndex].position, Quaternion.identity);
		pans[1].SetActive(false);
		pans[0].SetActive(true);
		spawnIndex++;
		buttons[val].interactable = false;	
	}
	public void DeleteBalls()
	{
		Instantiate(bulletPrefab, bulletPos.position, Quaternion.identity);
		Instantiate(bulletPrefab, bulletPos2.position, Quaternion.identity);
		spawnIndex = 0;
		ActivateButton();
	
	}
	void CheckTotalNumbers()
	{
		if(spawnIndex == spawnPos.Length)
		{
			insertBut.interactable = false;
			drawBut.interactable = true;
		}else
		{
			insertBut.interactable = true;
			drawBut.interactable = false;
			
		}
		
	}
	void ActivateButton()
	{
		foreach(Button butt in buttons)
		{
			if( butt.interactable == false)
			{
				butt.interactable = true;
			}
		}
	}
	public void Draw()
	{
		if(saveData.points > 1)
		{
			isReloading1 = true;
			pans[0].SetActive(false);
			platforms.SetActive(false);
			saveData.AddPoint(-dataPoints);
			SaveSystems.controls.SaveGame(saveData);
			StartCoroutine(BallSorter());
			
		}else{
			pans[3].SetActive(true);
			pans[0].SetActive(false);
		}
	}
	public void ExistReward()
	{
		pans[3].SetActive(false);
		pans[0].SetActive(true);
		
	}
	void ReloadOne()
	{
		pans[5].SetActive(true);
		if(reloadAmout1 < interval1)
		{
			reloadAmout1 += speed * Time.deltaTime;
			reloadOneText.text = reloadAmout1.ToString("0")+"%";
		}else{
			reloadOneText.text = "DONE";
			isReloading1 = false;
			reloadAmout1 = 0;
			pans[5].SetActive(false);
			pans[2].SetActive(true);
			
		}
		reloadImage1.fillAmount = reloadAmout1/interval1;
		
	}
	
	public void ReloadTwo()
	{
		pans[6].SetActive(true);
		if(reloadAmout2 < interval2)
		{
			reloadAmout2 += speed * Time.deltaTime;
			
		}else{
			
			isReloading2 = false;
			reloadAmout2 = 0;
			pans[6].SetActive(false);
			pans[0].SetActive(true);
			
		}
		reloadImage2.fillAmount = reloadAmout2/interval2;
		
	}
	public void Replay()
	{
		if(mySpawnerA.ballList.Count > 0)
		{
			mySpawnerA.ballList.Clear();
			Array.Clear(ballTypeExist, 0, ballTypeExist.Length);
			sortIndex = 0;
			
		}
		
		Instantiate(bulletPrefab2, bulletPosition.position, Quaternion.identity);
		
		pans[2].SetActive(false);
		isReloading2 = true;
		spawnIndex = 0;
		ActivateButton();
		platforms.SetActive(true);
		
	}
	public void RewardPoint()
	{
		saveData.AddPoint(rewardCoins);
	}
	void OnApplicationFocus(bool inFocus)
	{
		if(!inFocus)
		{
			SaveSystems.controls.SaveGame(saveData);
		}
	}
	
	void OnApplicationQuit()
	{
		SaveSystems.controls.SaveGame(saveData);
		
	}	
	public void Rate()
	{
		pans[4].SetActive(true);
	
	}
	public void RateMeNow()
	{
		pans[4].SetActive(false);
		Application.OpenURL("market://details?id=com.bolofly.PermKing");
		
	}
	public void Later()
	{
		pans[4].SetActive(false);
	
	}
	public void ExistApp()
	{
		Application.Quit();
		
	}
	public void RetainApp()
	{
		pans[7].SetActive(false);
		
	}
	public void Reset()
	{
		saveData.ResetPoint();
	}
	IEnumerator BallSorter()
	{
		
		yield return new WaitForSeconds(sortDuration);
		
		
		for(int i = 0; i < mySpawnerA.ballList.Count; i++)
		{
			for(int j = i + 1 ; j < mySpawnerA.ballList.Count; j++)
			{
				if(mySpawnerA.ballList[j].ballType == mySpawnerA.ballList[i].ballType && ballTypeExist[mySpawnerA.ballList[i].ballType] == false )
				{
					Instantiate( myBallPrefabs[mySpawnerA.ballList[i].ballType], sortPoint[sortIndex].position, Quaternion.identity);
					ballTypeExist[mySpawnerA.ballList[i].ballType] = true;
					sortIndex ++;
				}
				
			}
		}
	}
	public void ChangeSpawnerPosA()// FOR A BUTTON.
	{
		spawnerPos.transform.position = positionB.position;
		algoA.gameObject.SetActive(false);
		algoB.gameObject.SetActive(true);
	}
	public void ChangeSpawnerPosB()//FOR B BUTTON.
	{
		spawnerPos.transform.position = positionA.position;
		algoB.gameObject.SetActive(false);
		algoA.gameObject.SetActive(true);
	}
	
	
	
	
		

}