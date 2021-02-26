using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum access{unoccupaied, occupied, lowObstacle, highObstacle}
[System.Serializable]
public class MyTile
{
	[SerializeField]
	int[] position;
	[SerializeField]
	TerrainType tileTerrainType;
	[SerializeField]
	bool isOccupied;

	public access getTileAccess()
	{
		return tileTerrainType.getAccess();
	}

	public Tile getTerrainTile()
	{
		return tileTerrainType.getTerrainTile();
	}

	public int[] getPosition()
	{
		return position;
	}

	public void setPosition(int x, int y)
	{
		position[0] = x;
		position[1] = y;
	}

	public bool checkIfOccupied()
	{
		return isOccupied;
	}

	public void setIsOccupied(bool b)
	{
		isOccupied = b;
	}

	public MyTile(TerrainType terrainType)
	{
		position = new int[2];
		tileTerrainType = terrainType;
		isOccupied = false;
	}
}
