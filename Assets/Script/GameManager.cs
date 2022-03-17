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
	public Transform[] bulletPos;
	public Transform bulletPos2;
	public int rewardCoins;
	public int dataPoints;
	public SpawnerA mySpawnerA;
	public Transform bulletPosition;
	public GameObject[] myBallPrefabs;
	public float sortDuration;
	public Transform[] sortPoint;
	public bool[] ballTypeExist;
	private GameData saveData = new GameData();
	public bool[] isFull;
	public Button[] buttArray;
	private List<int> bulletList;
	
	
	void Awake(){
		instance = this;
		
	}
    void Start()
    {
		bulletList = new List<int>();
		bulletList.Add(99);
		bulletList.Add(99);
		bulletList.Add(99);
		bulletList.Add(99);
		bulletList.Add(99);
		bulletList.Add(99);
		bulletList.Add(99);
		bulletList.Add(99);
		bulletList.Add(99);
		bulletList.Add(99);
		
		
		foreach(GameObject pan in pans)
		{
			pan.SetActive(false);
		}
		foreach(Button butt in buttArray)
		{
			butt.gameObject.SetActive(false);
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
		for(int i = 0; i < spawnPos.Length; i++)
		{
			if(!isFull[i])
			{
				Instantiate(balls[val], spawnPos[i].position, Quaternion.identity);
				pans[1].SetActive(false);
				pans[0].SetActive(true);
				buttArray[i].gameObject.SetActive(true);
				spawnIndex++;
				buttons[val].interactable = false;
				bulletList.RemoveAt(i);
				CheckDuplicate(i, val);
				isFull[i] = true;
				
				break;
			}
			
		}	
		
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
	
	public void Draw()
	{
		DrawGame();
	
	}
	
	
	void DrawGame()
	{
		if(saveData.points > 1)
		{
			isReloading1 = true;
			pans[0].SetActive(false);
			platforms.SetActive(false);
			saveData.AddPoint(-dataPoints);
			SaveSystems.controls.SaveGame(saveData);
			StartCoroutine(BallSorter());
			bulletList.Clear();
			
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
			Array.Clear(isFull, 0, isFull.Length);
			sortIndex = 0;
			bulletList.Add(99);
			bulletList.Add(99);
			bulletList.Add(99);
			bulletList.Add(99);
			bulletList.Add(99);
			bulletList.Add(99);
			bulletList.Add(99);
			bulletList.Add(99);
			bulletList.Add(99);
			bulletList.Add(99);
			ActivateButton();
		}
		
		Instantiate(bulletPrefab2, bulletPosition.position, Quaternion.identity);
		
		pans[2].SetActive(false);
		isReloading2 = true;
		spawnIndex = 0;
		platforms.SetActive(true);
		
	}
	void CheckDuplicate(int index, int val)
	{
		bulletList.Insert(index, val);
	}
	
	void ActivateButton()
	{
		foreach(Button butt in buttons)
		{
			if(butt.interactable == false)
			{
				butt.interactable = true;
			}
		}
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
	
	public void DeleteOne()
	{
		
		Instantiate(bulletPrefab, bulletPos[0].position, Quaternion.identity);
		isFull[0] = false;
		buttons[bulletList[0]].interactable = true;
		bulletList.RemoveAt(0);
		bulletList.Insert(0, 99);
		buttArray[0].gameObject.SetActive(false);
		spawnIndex--;
		ShowButtonsPan();
		
	}
	public void DeleteTwo()
	{
		
		Instantiate(bulletPrefab, bulletPos[1].position, Quaternion.identity);
		isFull[1] = false;
		buttons[bulletList[1]].interactable = true;
		bulletList.RemoveAt(1);
		bulletList.Insert(1, 99);
		buttArray[1].gameObject.SetActive(false);
		spawnIndex--;
		ShowButtonsPan();
		
	}
	public void DeleteThree()
	{
		
		Instantiate(bulletPrefab, bulletPos[2].position, Quaternion.identity);
		isFull[2] = false;
		buttons[bulletList[2]].interactable = true;
		bulletList.RemoveAt(2);
		bulletList.Insert(2, 99);
		buttArray[2].gameObject.SetActive(false);
		spawnIndex--;
		ShowButtonsPan();
		
	}
	public void DeleteFour()
	{
		Instantiate(bulletPrefab, bulletPos[3].position, Quaternion.identity);
		isFull[3] = false;
		buttons[bulletList[3]].interactable = true;
		bulletList.RemoveAt(3);
		bulletList.Insert(3, 99);
		buttArray[3].gameObject.SetActive(false);
		spawnIndex--;
		ShowButtonsPan();
		
	}
	public void DeleteFive()
	{
		
		Instantiate(bulletPrefab, bulletPos[4].position, Quaternion.identity);
		isFull[4] = false;
		buttons[bulletList[4]].interactable = true;
		bulletList.RemoveAt(4);
		bulletList.Insert(4, 99);
		buttArray[4].gameObject.SetActive(false);
		spawnIndex--;
		ShowButtonsPan();
		
		
	}
	public void DeleteSix()
	{
		
		Instantiate(bulletPrefab, bulletPos[5].position, Quaternion.identity);
		isFull[5] = false;
		buttons[bulletList[5]].interactable = true;
		bulletList.RemoveAt(5);
		bulletList.Insert(5, 99);
		buttArray[5].gameObject.SetActive(false);
		spawnIndex--;
		ShowButtonsPan();
	}
	public void DeleteSeven()
	{
		
		Instantiate(bulletPrefab, bulletPos[6].position, Quaternion.identity);
		isFull[6] = false;
		buttons[bulletList[6]].interactable = true;
		bulletList.RemoveAt(6);
		bulletList.Insert(6, 99);
		buttArray[6].gameObject.SetActive(false);
		spawnIndex--;
		ShowButtonsPan();
	
	}
	public void DeleteEight()
	{
		
		Instantiate(bulletPrefab, bulletPos[7].position, Quaternion.identity);
		isFull[7] = false;
		buttons[bulletList[7]].interactable = true;
		bulletList.RemoveAt(7);
		bulletList.Insert(7, 99);
		buttArray[7].gameObject.SetActive(false);
		spawnIndex--;
		ShowButtonsPan();
		
	}
	public void DeleteNine()
	{
		
		Instantiate(bulletPrefab, bulletPos[8].position, Quaternion.identity);
		isFull[8] = false;
		buttons[bulletList[8]].interactable = true;
		bulletList.RemoveAt(8);
		bulletList.Insert(8, 99);
		buttArray[8].gameObject.SetActive(false);
		spawnIndex--;
		ShowButtonsPan();
		
		
	}
	public void DeleteTen()
	{
		
		Instantiate(bulletPrefab, bulletPos[9].position, Quaternion.identity);
		isFull[9] = false;
		buttons[bulletList[9]].interactable = true;
		bulletList.RemoveAt(9);
		bulletList.Insert(9, 99);
		buttArray[9].gameObject.SetActive(false);
		spawnIndex--;
		ShowButtonsPan();
	}
	
	
		
	
}	