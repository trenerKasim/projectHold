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

	[SerializeField]
	PawnModel model;
	[SerializeField]
	Sprite modelSprite;

	public int getSpeed()
	{
		return baseSpeed;
	}

	public int getAttackRange(int weaponLength)
	{
		switch(size)
		{
			case CreatureSize.Short:
			{
				switch(weaponLength)
				{
					case 1:
					case 2:
					case 3:
					case 4:
						return 1;
					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
						return 2;
					default:
						return 3;
				}
			}
		}
		return -1;
	}

	//----------------------------------Visuals Getters
	public PawnModel getModel()
	{
		return model;
	}
	public Sprite getModelSprite()
	{
		return modelSprite;
	}
}
