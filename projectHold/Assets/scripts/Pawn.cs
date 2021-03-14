using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PawnModel {dwarf, goblin};
public class Pawn : MonoBehaviour {
	[SerializeField]
	PawnModel modelType;

	[SerializeField]
	SpriteRenderer pawnBody;
	[SerializeField]
	GameObject helmet;
	[SerializeField]
	GameObject armor;
	[SerializeField]
	GameObject weapon;
	[SerializeField]
	SpriteRenderer miniMapIndicator;


	public PawnModel getPawnModelType()
	{
		return modelType;
	}

	public GameObject getHelmet() {
		return helmet;
	}

	public GameObject getArmor() {
		return armor;
	}

	public GameObject getWeapon() {
		return weapon;
	}

	public void setPawnModel(PawnModel newModelType, Sprite newModel)
	{
		modelType = newModelType;
		pawnBody.sprite = newModel;
	}

	public void setIndicator(Color color)
	{
		miniMapIndicator.color = color;
	}
}
