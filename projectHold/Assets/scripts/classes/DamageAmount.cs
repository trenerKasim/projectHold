using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageAmount
{
	[SerializeField]
	int baseDamage;
	[SerializeField]
	Dice randomDamage;

	public int damageInput()
	{
		return baseDamage + randomDamage.roll();
	}
}
