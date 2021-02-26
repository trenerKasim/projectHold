using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
	[SerializeField]
	GameObject armor;
	[SerializeField]
	GameObject weapon;

	public GameObject getArmor()
	{
		return armor;
	}

	public GameObject getWeapon()
	{
		return weapon;
	}
}
