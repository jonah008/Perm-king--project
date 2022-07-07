using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOne : MonoBehaviour
{
	
	
	
	
	void OnCollisionEnter2D(Collision2D col)
	{
		switch(col.gameObject.tag)
		{
			case "One":
			GameManager.instance.boxOne = 2;
			
			break;
			case "Two":
			GameManager.instance.boxOne = 3;
			
			break;
			case "Three":
			GameManager.instance.boxOne = 4;
			
			break;
			case "Four":
			GameManager.instance.boxOne = 5;
			
			break;
			case "Five":
			GameManager.instance.boxOne = 6;
		

			break;
			case "Six":
			GameManager.instance.boxOne = 7;
			
			break;
			case "Seven":
			GameManager.instance.boxOne = 8;
			
			break;
			case "Eight":
			GameManager.instance.boxOne = 9;
		
			break;
			case "Nine":
			GameManager.instance.boxOne = 10;
		
			break;
			case "Ten":
			GameManager.instance.boxOne = 11;
			
			break;
			case "Eleven":
			GameManager.instance.boxOne = 12;
			
			break;
			case "Twelve":
			GameManager.instance.boxOne = 13;
		
			break;
			case "Thirteen":
			GameManager.instance.boxOne = 14;
			
			break;
			case "Fourteen":
			GameManager.instance.boxOne = 15;
		
			break;
			case "Fifteen":
			GameManager.instance.boxOne = 16;
			
			break;
			case "Sixteen":
			GameManager.instance.boxOne = 17;
			
			break;
			case "Seventeen":
			GameManager.instance.boxOne = 18;
			
			break;
			case "Eighteen":
			GameManager.instance.boxOne = 19;
			
			break;
			case "Nineteen":
			GameManager.instance.boxOne = 20;
			
			break;
			case "Twenty":
			GameManager.instance.boxOne = 21;
			
			break;
			case "TwentyOne":
			GameManager.instance.boxOne = 22;
			
			break;
			case "TwentyTwo":
			GameManager.instance.boxOne = 23;
			
			break;
			case "TwentyThree":
			GameManager.instance.boxOne = 24;
			
			break;
			case "TwentyFour":
			GameManager.instance.boxOne = 25;
			
			break;
			case "TwentyFive":
			GameManager.instance.boxOne = 26;
			
			break;
			case "TwentySix":
			GameManager.instance.boxOne = 27;
			
			break;
			case "TwentySeven":
			GameManager.instance.boxOne = 28;
			
			break;
			case "TwentyEight":
			GameManager.instance.boxOne = 29;
			
			break;
			case "TwentyNine":
			GameManager.instance.boxOne = 30;
		
			break;
			case "Thirty":
			GameManager.instance.boxOne = 31;
			
			break;
			case "ThirtyOne":
			GameManager.instance.boxOne = 32;
		
			break;
			case "ThirtyTwo":
			GameManager.instance.boxOne = 33;
			
			break;
			case "ThirtyThree":
			GameManager.instance.boxOne = 34;
			
			break;
			case "ThirtyFour":
			GameManager.instance.boxOne = 35;
			
			break;
			case "ThirtyFive":
			GameManager.instance.boxOne = 36;
			
			break;
			case "ThirtySix":
			GameManager.instance.boxOne = 37;
			
			break;
			case "ThirtySeven":
			GameManager.instance.boxOne = 38;
			
			break;
			case "ThirtyEight":
			GameManager.instance.boxOne = 39;
			
			break;
			case "ThirtyNine":
			GameManager.instance.boxOne = 40;
			

			break;
			case "Fourty":
			GameManager.instance.boxOne = 41;
			break;
			case "FourtyOne":
			GameManager.instance.boxOne = 42;
			break;
			case "FourtyTwo":
			GameManager.instance.boxOne = 43;
			break;
			case "FourtyThree":
			GameManager.instance.boxOne = 44;
			break;
			case "FourtyFour":
			GameManager.instance.boxOne = 45;
			break;
			case "FourtyFive":
			GameManager.instance.boxOne = 46;
			break;
			case "FourtySix":
			GameManager.instance.boxOne = 47;
			break;
			case "FourtySeven":
			GameManager.instance.boxOne = 48;
			break;
			case "FourtyEight":
			GameManager.instance.boxOne = 49;
			break;
			case "FourtyNine":
			GameManager.instance.boxOne = 50;
			
			break;
			case "Fifty":
			GameManager.instance.boxOne = 51;
			break;
			case "FiftyOne":
			GameManager.instance.boxOne = 52;
		
			break;
			case "FiftyTwo":
			GameManager.instance.boxOne = 53;
			break;
			case "FiftyThree":
			GameManager.instance.boxOne = 54;
			break;
			case "FiftyFour":
			GameManager.instance.boxOne = 55;
			break;
			
			case "FiftyFive":
			GameManager.instance.boxOne = 56;
			
			break;
			case "FiftySix":
			GameManager.instance.boxOne = 57;
			break;
			case "FiftySeven":
			GameManager.instance.boxOne = 58;
			break;
			case "FiftyEight":
			GameManager.instance.boxOne = 59;
			break;
			case "FiftyNine":
			GameManager.instance.boxOne = 60;
			break;
			case "Sixty":
			GameManager.instance.boxOne = 61;
			break;
			case "SixtyOne":
			GameManager.instance.boxOne = 62;
			break;
			case "SixtyTwo":
			GameManager.instance.boxOne = 63;
			break;
			case "SixtyThree":
			GameManager.instance.boxOne = 64;
			break;
			case "SixtyFour":
			GameManager.instance.boxOne = 65;
		
			break;
			case "SixtyFive":
			GameManager.instance.boxOne = 66;
			break;
			case "SixtySix":
			GameManager.instance.boxOne = 67;
			
			break;
			case "SixtySeven":
			GameManager.instance.boxOne = 68;
			
			break;
			case "SixtyEight":
			GameManager.instance.boxOne = 69;
			break;
			case "SixtyNine":
			GameManager.instance.boxOne = 70;
	
			break;
			case "Seventy":
			GameManager.instance.boxOne = 71;
			break;
			case "SeventyOne":
			GameManager.instance.boxOne = 72;
			break;
			case "SeventyTwo":
			GameManager.instance.boxOne = 73;
			break;
			case "SeventyThree":
			GameManager.instance.boxOne = 74;
			break;
			case "SeventyFour":
			GameManager.instance.boxOne = 75;
			
			break;
			case "SeventyFive":
			GameManager.instance.boxOne = 76;
			break;
			case "SeventySix":
			GameManager.instance.boxOne = 77;
			break;
			case "SeventySeven":
			GameManager.instance.boxOne = 78;
			break;
			case "SeventyEight":
			GameManager.instance.boxOne = 79;
			break;
			case "SeventyNine":
			GameManager.instance.boxOne = 80;
			
			break;
			case "Eighty":
			GameManager.instance.boxOne = 81;
			break;
			case "EightyOne":
			GameManager.instance.boxOne = 82;
			break;
			case "EightyTwo":
			GameManager.instance.boxOne = 83;
			break;
			case "EightyThree":
			GameManager.instance.boxOne = 84;
			
			break;
			case "EightyFour":
			GameManager.instance.boxOne = 85;
			break;
			case "EightyFive":
			GameManager.instance.boxOne = 86;
			break;
			case "EightySix":
			GameManager.instance.boxOne = 87;
			break;
			case "EightySeven":
			GameManager.instance.boxOne = 88;
			break;
			case "EightyEight":
			GameManager.instance.boxOne = 89;
			break;
			case "EightyNine":
			GameManager.instance.boxOne = 90;
			break;
			case "Ninety":
			GameManager.instance.boxOne = 90;
		
			break;
			
			
		}
	}
   
}