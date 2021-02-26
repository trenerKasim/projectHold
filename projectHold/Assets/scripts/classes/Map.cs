using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum light {day, night}
[System.Serializable]
public class Map
{
	[SerializeField]
	int[] size;
	[SerializeField]
	MyTile[,] tiles;
	[SerializeField]
	light mapLight;

	public int[] getSize()
	{
		return size;
	}

	public void setSize(int[] newSize)
	{
		size = newSize;
		tiles = new MyTile[size[0], size[1]];
	}

	public MyTile getTile(int x, int y)
	{
		return tiles[x,y];
	}

	public void setTile(int x, int y, TerrainType terrainType)
	{
		tiles[x, y] = new MyTile(terrainType);
	}

	public light getMapLight()
	{
		return mapLight;
	}
}
