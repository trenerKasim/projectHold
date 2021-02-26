using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
[CreateAssetMenu(fileName = "TerrainType", menuName = "ScriptableObjects/newTerrain", order = 1)]
public class TerrainType : ScriptableObject
{
	public new string name;
	[SerializeField]
	access tileAccess;
	[SerializeField]
	Tile tile;

	public Tile getTerrainTile()
	{
		return tile;
	}

	public access getAccess()
	{
		return tileAccess;
	}
}
