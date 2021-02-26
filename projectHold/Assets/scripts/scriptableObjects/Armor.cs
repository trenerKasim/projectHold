using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Armor", menuName = "ScriptableObjects/newArmor", order = 1)]
public class Armor : ScriptableObject
{
	public new string name;
	[SerializeField]
	int slashResistance;
	[SerializeField]
	int pierceResistance;
	[SerializeField]
	int bluntResistance;

	[SerializeField]
	Sprite sprite;

	public Sprite getSprite()
	{
		return sprite;
	}
}
