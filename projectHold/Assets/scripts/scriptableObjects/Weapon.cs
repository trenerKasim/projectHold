using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/newWeapon", order = 1)]
public class Weapon : ScriptableObject
{
	public new string name;
	[SerializeField]
	int lenght;
	[SerializeField]
	DamageAmount slashDamage;
	[SerializeField]
	DamageAmount pierceDamage;
	[SerializeField]
	DamageAmount bluntDamage;
	[SerializeField]
	float bluntMultiplayer; //<< rawBluntDamage = bluntDamage + (slashDamageReducedByArmor + pierceDamageReducedByArmor) * bluntMultiplayer
	[SerializeField]
	Sprite sprite;

	public int getLeght()
	{
		return lenght;
	}

	public Sprite getSprite()
	{
		return sprite;
	}
}
