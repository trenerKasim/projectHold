using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Unit
{
	[Header("Base info")]
	public string name;
	[SerializeField]
	Race race;
	[SerializeField][Range(0,1)]
	int side;
	[Header("Battle info")]
	[SerializeField]
	int[] position;
	[SerializeField][Range(0,2)]
	int actions;
	[SerializeField]
	int initiative;
	[Header("Stats")]
	[SerializeField]
	int[] healthPoints = new int[2];//[0]<<Current,[1]<<Max
	[Header("Equipment")]
	[SerializeField]
	Weapon weapon;
	[SerializeField]
	Helmet helmet;
	[SerializeField]
	Armor armor;
	[Header("Visuals")]
	[SerializeField]
	GameObject pawn;
	[SerializeField]
	GameObject pawnHelmet;
	[SerializeField]
	GameObject pawnArmor;
	[SerializeField]
	GameObject pawnWeapon;

	public string getName()
	{
		return name;
	}

	public Race getRace()
	{
		return race;
	}

	public int getSide()
	{
		return side;
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
		pawn.GetComponent<Pawn>().setPawnModel(race.getModel(),race.getModelSprite());
		pawnHelmet = pawn.GetComponent<Pawn>().getHelmet();
		setHelmetSprite();
		pawnArmor = pawn.GetComponent<Pawn>().getArmor();
		setArmorSprite();
		pawnWeapon = pawn.GetComponent<Pawn>().getWeapon();
		setWeaponSprite();
		if (side == 1)
		{
			pawn.GetComponent<Pawn>().setIndicator(Color.red);
		}
	}

	public void setHelmetSprite()
	{
		if (helmet != null)
		{
			pawnHelmet.GetComponent<SpriteRenderer>().sprite = helmet.getSprite();
		}
		else
		{
			pawnHelmet.GetComponent<SpriteRenderer>().sprite = null;
		}
	}

	public GameObject getPawn()
	{
		return pawn;
	}

	public void setWeaponSprite()
	{
		pawnWeapon.GetComponent<SpriteRenderer>().sprite = weapon.getSprite();
	}

	public Weapon getWeapon()
	{
		return weapon;
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
