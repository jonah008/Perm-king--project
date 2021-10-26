using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
	public int points = 100;

    
    public void AddPoint(int amount)
	{
		points += amount;
	}
	
	 public void ResetPoint()
	{
		points = 100;
	}	
}
