using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dice
{
	[SerializeField]
	int diceCount;
	[SerializeField]
	int diceSides;

	public int roll()
	{
		int result = 0;
		for(int i = 0; i < diceCount; i++)
		{
			result += Random.Range(1, diceSides);
		}
		return result;
	}
}
