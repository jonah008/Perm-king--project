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
	public Transform bulletPosition;
	public GameObject[] myBallPrefabs;
	public float sortDuration;
	public Transform[] sortPoint;
	public Button randomButton;
	private GameData saveData = new GameData();
	public bool[] isFull;
	public Button[] buttArray;
	private List<int> bulletList;
	public int boxOne, boxTwo, boxThree, boxFour, boxFive, boxSix, boxSeven, boxEight, boxNine, boxTen;
	private List<Balls> finalList;
	public NationalChart nationalChart;
	private List<Balls> lastList;
	public GameObject forecastLabel;
	
	void Awake(){
		instance = this;
		
	}
    void Start()
    {
		forecastLabel.SetActive(false);
		
		randomButton.gameObject.SetActive(false);
		
		finalList = new List<Balls>();
		lastList = new List<Balls>();
		
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
		boxOne = 0;
		boxTwo = 0;
		boxThree = 0;
		boxFour = 0;
		boxFive = 0;
		boxSix = 0;
		boxSeven = 0;
		boxEight = 0;
		boxNine = 0;
		boxTen = 0;
		sortIndex = 0;
		
		
    }

    
    void Update()
    {
		pointText.text = "POINT:" + saveData.points.ToString();
		if(isReloading1){

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
	
	private void CheckTotalNumbers()
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
	
	
	private void DrawGame()
	{
		if(saveData.points > 1)
		{
			
			isReloading1 = true;
			pans[0].SetActive(false);
			platforms.SetActive(false);
			forecastLabel.SetActive(true);
			saveData.AddPoint(-dataPoints);
			SaveSystems.controls.SaveGame(saveData);
			StartCoroutine(SortAllMethod());
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
		forecastLabel.SetActive(false);
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
		
		Instantiate(bulletPrefab2, bulletPosition.position, Quaternion.identity);
		
		pans[2].SetActive(false);
		isReloading2 = true;
		spawnIndex = 0;
		platforms.SetActive(true);
		finalList.Clear();
		Array.Clear(nationalChart.indexAdded, 0, nationalChart.indexAdded.Length);
		randomButton.gameObject.SetActive(false);
		
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
	public void GenerateRandom()
	{
		randomButton.gameObject.SetActive(false);
		int totalLoopTime = 10;
		
		for(int i = 0; i < totalLoopTime; i++)
		{
			int randInt = UnityEngine.Random.Range(2, 89);
			Instantiate(myBallPrefabs[randInt], sortPoint[sortIndex].position, Quaternion.identity);
			sortIndex++;
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
	public void DeleteTen(){
		
		Instantiate(bulletPrefab, bulletPos[9].position, Quaternion.identity);
		isFull[9] = false;
		buttons[bulletList[9]].interactable = true;
		bulletList.RemoveAt(9);
		bulletList.Insert(9, 99);
		buttArray[9].gameObject.SetActive(false);
		spawnIndex--;
		ShowButtonsPan();
		boxTen = 0;
		
	}
	void FirstMethodOne()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].firstBox == boxOne && nationalChart.ballList[i].secondBox == boxTwo && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void FirstMethodTwo()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].firstBox == boxOne && nationalChart.ballList[i].thirdBox == boxThree && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void FirstMethodThree()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].firstBox == boxOne && nationalChart.ballList[i].fourthBox == boxFour && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void FirstMethodFour()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].firstBox == boxOne && nationalChart.ballList[i].fifthBox == boxFive && nationalChart.indexAdded[i] == false )
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void FirstMethodFive()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].firstBox == boxOne && nationalChart.ballList[i].sixthBox == boxSix && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void FirstMethodSix()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].firstBox == boxOne && nationalChart.ballList[i].seventhBox == boxSeven && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void FirstMethodSeven()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].firstBox == boxOne && nationalChart.ballList[i].eightBox == boxEight && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void FirstMethodEight()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].firstBox == boxOne && nationalChart.ballList[i].ninethBox == boxNine && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void FirstMethodNine()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].firstBox == boxOne && nationalChart.ballList[i].tenthBox == boxTen && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void SecondMethodOne()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].secondBox == boxTwo && nationalChart.ballList[i].thirdBox == boxThree && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void SecondMethodTwo()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].secondBox == boxTwo && nationalChart.ballList[i].fourthBox == boxFour && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void SecondMethodThree()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].secondBox == boxTwo && nationalChart.ballList[i].fifthBox == boxFive && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void SecondMethodFour()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].secondBox == boxTwo && nationalChart.ballList[i].sixthBox == boxSix && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void SecondMethodFive()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].secondBox == boxTwo && nationalChart.ballList[i].seventhBox == boxSeven && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void SecondMethodSix()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].secondBox == boxTwo && nationalChart.ballList[i].eightBox == boxEight && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void SecondMethodSeven()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].secondBox == boxTwo && nationalChart.ballList[i].ninethBox == boxNine && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void SecondMethodEight()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].secondBox == boxTwo && nationalChart.ballList[i].tenthBox == boxTen && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	
	
	void ThirdMethodOne()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].thirdBox == boxThree && nationalChart.ballList[i].fourthBox == boxFour && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void ThirdMethodTwo()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].thirdBox == boxThree && nationalChart.ballList[i].fifthBox == boxFive && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void ThirdMethodThree()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].thirdBox == boxThree && nationalChart.ballList[i].sixthBox == boxFive && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}	
			}
			
		}
	}
	void ThirdMethodFour()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].thirdBox == boxThree && nationalChart.ballList[i].seventhBox == boxSeven && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void ThirdMethodFive()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].thirdBox == boxThree && nationalChart.ballList[i].eightBox == boxEight && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void ThirdMethodSix()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].thirdBox == boxThree && nationalChart.ballList[i].ninethBox == boxNine && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void ThirdMethodSeven()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].thirdBox == boxThree && nationalChart.ballList[i].tenthBox == boxTen && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void FourthMethodOne()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fourthBox == boxFour && nationalChart.ballList[i].fifthBox == boxFive && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
		}
	}
	void FourthMethodTwo()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fourthBox == boxFour && nationalChart.ballList[i].sixthBox == boxSix && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
		}
	}
	void FourthMethodThree()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fourthBox == boxFour && nationalChart.ballList[i].seventhBox == boxSeven && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
		}
	}
	void FourthMethodFour()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fourthBox == boxFour && nationalChart.ballList[i].eightBox == boxEight && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
		}
	}
	void FourthMethodFive()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fourthBox == boxFour && nationalChart.ballList[i].ninethBox == boxNine && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
		}
	}
	void FourthMethodSix()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fourthBox == boxFour && nationalChart.ballList[i].tenthBox == boxTen && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
		}
	}
	void FifthMethodOne()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fifthBox == boxFive && nationalChart.ballList[i].sixthBox == boxSix && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void FifthMethodTwo()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fifthBox == boxFive && nationalChart.ballList[i].seventhBox == boxSeven && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
	}
	void FifthMethodThree()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fifthBox == boxFive && nationalChart.ballList[i].eightBox == boxEight && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
	}
	void FifthMethodFour()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fifthBox == boxFive && nationalChart.ballList[i].ninethBox == boxNine && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
	}
	void FifthMethodFive()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fifthBox == boxFive && nationalChart.ballList[i].tenthBox == boxTen && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
	}
	void FirstMachineOne()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].sixthBox == boxSix && nationalChart.ballList[i].seventhBox == boxSeven && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void FirstMachineTwo()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].sixthBox == boxSix && nationalChart.ballList[i].eightBox == boxEight && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
	}
	void FirstMachineThree()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].sixthBox == boxSix && nationalChart.ballList[i].ninethBox == boxNine && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void FirstMachineFour()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].sixthBox == boxSix && nationalChart.ballList[i].tenthBox == boxTen && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	
	void SecondMachineOne()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].seventhBox == boxSeven && nationalChart.ballList[i].eightBox == boxEight && nationalChart.indexAdded[i] == false )
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void SecondMachineTwo()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].seventhBox == boxSeven && nationalChart.ballList[i].ninethBox == boxNine && nationalChart.indexAdded[i] == false )
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void SecondMachineThree()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].seventhBox == boxSeven && nationalChart.ballList[i].tenthBox == boxTen && nationalChart.indexAdded[i] == false )
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void ThirdMachineOne()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].eightBox == boxEight && nationalChart.ballList[i].ninethBox == boxNine && nationalChart.indexAdded[i] == false )
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void ThirdMachineTwo()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].eightBox == boxEight && nationalChart.ballList[i].tenthBox == boxTen && nationalChart.indexAdded[i] == false )
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void FourthMachineOne()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].ninethBox == boxNine && nationalChart.ballList[i].tenthBox == boxTen && nationalChart.indexAdded[i] == false )
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}	
		
	}
	void SecondRandomMethodOne()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].secondBox == boxTwo && nationalChart.ballList[i].firstBox == boxThree && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
		}	
	}
	void SecondRandomMethodTwo()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].secondBox == boxTwo && nationalChart.ballList[i].thirdBox == boxOne && nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
				
			}
			
		}
		
	}
	
	void ThirdRandomMethodOne()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].thirdBox == boxThree && nationalChart.ballList[i].secondBox == boxFour &&  nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
		}	
	}
	void ThirdRandomMethodTwo()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].thirdBox == boxThree && nationalChart.ballList[i].fourthBox == boxTwo &&  nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
				
			}
			
		}
		
	}
	
	
	void FourthRandomMethodOne()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fourthBox == boxFour && nationalChart.ballList[i].thirdBox == boxFive &&  nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
			}
			
		}
		
	}
	void FourthRandomMethodTwo()
	{
		for(int i = 0; i < nationalChart.ballList.Count; i++)
		{
			int nextIndex = i+1;
			if(nationalChart.ballList[i].fourthBox == boxFour && nationalChart.ballList[i].fifthBox == boxThree &&  nationalChart.indexAdded[i] == false)
			{
				if(nextIndex < nationalChart.ballList.Count)
				{
					finalList.Add(nationalChart.ballList[nextIndex]);
					nationalChart.indexAdded[i] = true;
				}
				
			}
			
		}
		
	}
	
	void GetSuccessNumbers()
	{
		
		if(finalList.Count > 0 )
		{
			
			for(int i = 0; i < finalList.Count; i++)
			{
				if(i == 6)
				{
					break;
				}
				
				
				Instantiate(myBallPrefabs[finalList[i].firstBox], sortPoint[sortIndex].position, Quaternion.identity);
				sortIndex++;
				Instantiate(myBallPrefabs[finalList[i].secondBox], sortPoint[sortIndex].position, Quaternion.identity);
				sortIndex++;
				Instantiate(myBallPrefabs[finalList[i].thirdBox], sortPoint[sortIndex].position, Quaternion.identity);
				sortIndex++;
				Instantiate(myBallPrefabs[finalList[i].fourthBox], sortPoint[sortIndex].position, Quaternion.identity);
				sortIndex++;
				Instantiate(myBallPrefabs[finalList[i].fifthBox], sortPoint[sortIndex].position, Quaternion.identity);
				sortIndex++;
					
			}
		}else{
			
			randomButton.gameObject.SetActive(true);
			
		}
		
	}
	
	
	IEnumerator SortAllMethod()
	{
		yield return new WaitForSeconds(sortDuration);
		
		SecondRandomMethodOne();
		SecondRandomMethodTwo();
		ThirdRandomMethodOne();
		ThirdRandomMethodTwo();
		FourthRandomMethodOne();
		FourthRandomMethodTwo();
		FirstMethodOne();
		FirstMethodTwo();
		FirstMethodThree();
		FirstMethodFour();
		//FirstMethodFive();
		//FirstMethodSix();
		//FirstMethodSeven();
		//FirstMethodEight();
		//FirstMethodNine();
		SecondMethodOne();
		SecondMethodTwo();
		SecondMethodThree();
		SecondMethodFour();
		SecondMethodFive();
		SecondMethodSix();
		SecondMethodSeven();
		SecondMethodEight();
		ThirdMethodOne();
		ThirdMethodTwo();
		ThirdMethodThree();
		ThirdMethodFour();
		//ThirdMethodFive();
		//ThirdMethodSix();
		//ThirdMethodSeven();
		FourthMethodOne();
		FourthMethodTwo();
		FourthMethodThree();
		FourthMethodFour();
		FourthMethodFive();
		FourthMethodSix();
		FifthMethodOne();
		FifthMethodTwo();
		FifthMethodThree();
		FifthMethodFour();
		FifthMethodFive();
		FirstMachineOne();
		FirstMachineTwo();
		FirstMachineThree();
		FirstMachineFour();
		SecondMachineOne();
		SecondMachineTwo();
		SecondMachineThree();
		ThirdMachineOne();
		ThirdMachineTwo();
		FourthMachineOne();
		
		GetSuccessNumbers();
		
	}

	
}