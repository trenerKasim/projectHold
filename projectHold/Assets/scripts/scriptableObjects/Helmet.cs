using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Helmet", menuName = "ScriptableObjects/newHelmet", order = 1)]
public class Helmet : ScriptableObject
{
	public new string name;
	[SerializeField]
	[Range(0f, 1f)]
	float coverage;
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
