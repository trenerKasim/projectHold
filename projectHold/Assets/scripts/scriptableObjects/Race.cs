using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CreatureSize {Small, Short, Average, Tall}
[System.Serializable]
[CreateAssetMenu(fileName = "Race", menuName = "ScriptableObjects/newRace", order = 1)]
public class Race : ScriptableObject
{
	public new string name;
	[SerializeField]
	int baseHealtPoints;
	[SerializeField]
	int baseSpeed;  //in tiles, 5 feet >> 1.5 m = one tile, base speed of human >> 6 >> 9m >> 30 feets
	[SerializeField]
	CreatureSize size;

	public int getSpeed()
	{
		return baseSpeed;
	}
}
