using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit
{
	public string name;
	[SerializeField]
	int[] position;
	[SerializeField][Range(0,2)]
	int actions;
	[SerializeField]
	int[] healthPoints = new int[2];//[0]<<Current,[1]<<Max
	[SerializeField]
	int initiative;
	[SerializeField]
	Race race;
	[SerializeField]
	Weapon weapon;
	[SerializeField]
	Armor armor;
	[SerializeField]
	GameObject pawn;
	[SerializeField]
	GameObject pawnArmor;
	[SerializeField]
	GameObject pawnWeapon;

	public string getName()
	{
		return name;
	}

	public int[] getPosition()
	{
		return position;
	}

	public int getActions()
	{
		return actions;
	}

	public void useAction()
	{
		actions--;
	}

	public void resetActions()
	{
		actions = 2;
	}

	public void setPawn(GameObject pawnPrefab)
	{
		pawn = GameObject.Instantiate(pawnPrefab, new Vector3(0,0,0), Quaternion.identity);
		pawnArmor = pawn.GetComponent<Pawn>().getArmor();
		pawnWeapon = pawn.GetComponent<Pawn>().getWeapon();
	}

	public GameObject getPawn()
	{
		return pawn;
	}

	public void setWeaponSprite()
	{
		pawnWeapon.GetComponent<SpriteRenderer>().sprite = weapon.getSprite();
	}

	public void setArmorSprite()
	{
		pawnArmor.GetComponent<SpriteRenderer>().sprite = armor.getSprite();
	}

	public void setPosition(int x, int y)
	{
		position[0] = x;
		position[1] = y;
	}

	public void setPawnPosition(Vector3 newPosition)
	{
		pawn.transform.position = newPosition;
	}

	public int getSpeed()
	{
		return race.getSpeed();
	}

	public void rollInitiative()
	{
		initiative = Random.Range(1, 20);
	}

	public int getInitiative()
	{
		return initiative;
	}
}
