using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.IO;

[System.Serializable]
public class mapManager : MonoBehaviour
{
	[SerializeField]
	battleManager battleManager;
	[SerializeField]
	TextAsset mapFile;
	[SerializeField]
	string mapFileString;
	[SerializeField]
	Map currentMap;
	[SerializeField]
	Tilemap tilemap;
	[SerializeField]
	Tile unoccupied;
	[SerializeField]
	Tile occupied;
	[SerializeField]
	Tile highObstacle;
	[SerializeField]
	Tilemap avalibityTilemap;
	[SerializeField]
	Tilemap rangeTilemap;

	[SerializeField]
	TerrainType[] terrainTypes;

	public void initialize()
	{
		battleManager = GetComponent<battleManager>();
	}

	public void ReadString()
	{
		string path = "Assets/scripts/map.txt";
		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader(path);
		string sizeReader = reader.ReadLine();
		string[] sizeReaderSplitted = sizeReader.Split(';');
		int[] sizeSetter = new int[2];
		sizeSetter[0] = int.Parse(sizeReaderSplitted[0]);
		sizeSetter[1] = int.Parse(sizeReaderSplitted[1]);
		currentMap.setSize(sizeSetter);
		for (int x = 0; x < currentMap.getSize()[0]; x++)
		{
			string line = reader.ReadLine();
			string[] lineTile = line.Split(';');
			for (int y = 0; y < currentMap.getSize()[1]; y++)
			{
				switch (lineTile[y])
				{
					case "G": currentMap.setTile(x, y, terrainTypes[0]); break;
					case "W": currentMap.setTile(x, y, terrainTypes[1]); break;
				}
			}
		}

		reader.Close();
	}

	public void generateMap()
    {
		ReadString();
		for(int x=0;x<currentMap.getSize()[0];x++)
		{
			for(int y=0;y<currentMap.getSize()[1];y++)
			{
				tilemap.SetTile(new Vector3Int(x, y, 0), currentMap.getTile(x, y).getTerrainTile());
				switch (currentMap.getTile(x, y).getTileAccess())
				{
					case access.unoccupaied: avalibityTilemap.SetTile(new Vector3Int(x, y, 0), unoccupied); break;
					case access.highObstacle: avalibityTilemap.SetTile(new Vector3Int(x, y, 0), highObstacle); break;
				}
				currentMap.getTile(x, y).setPosition(x, y);
			}
		}
    }

	public void avalibityMapReset(bool unitsPlaced)
	{
		for (int x = 0; x < currentMap.getSize()[0]; x++)
		{
			for (int y = 0; y < currentMap.getSize()[1]; y++)
			{
				switch (currentMap.getTile(x, y).getTileAccess())
				{
					case access.unoccupaied: avalibityTilemap.SetTile(new Vector3Int(x, y, 0), unoccupied); break;
					case access.highObstacle: avalibityTilemap.SetTile(new Vector3Int(x, y, 0), highObstacle); break;
				}
				currentMap.getTile(x, y).setPosition(x, y);
			}
		}
		if (unitsPlaced)
		{
			foreach (Unit unit in battleManager.getAllUnits())
			{
				placeUnit(unit.getPosition());
			}
		}
	}
    
	public void placeUnit(int[] tempPosition) //<< change into placeUnits(Unit[])
	{
		//avalibityMapReset();
		//Debug.Log("に");
		for(int x=0;x<currentMap.getSize()[0];x++)
		{
			for(int y=0;y<currentMap.getSize()[1];y++)
			{
				if(x == tempPosition[0] && y == tempPosition[1])
				{
					//Debug.Log("いち");
					avalibityTilemap.SetTile(new Vector3Int(x, y, 0), occupied);
					currentMap.getTile(x, y).setIsOccupied(true);
				}
			}
		}
	}

	public void generateRangeTile(int x, int y, Tile tile)
	{
		rangeTilemap.SetTile(new Vector3Int(x, y, 0), tile);
	}

	public void moveRangeGenerate(Unit unit)
	{
		clearRangeTilemap();
		AStar aStar= new AStar(currentMap.getSize(),this, aStarMode.move);
		aStar.generateRange(unit.getPosition()[0], unit.getPosition()[1], unit.getSpeed(),occupied);
	}

	public void attackRangeGenerate(Unit unit)
	{
		clearRangeTilemap();
		AStar aStar = new AStar(currentMap.getSize(), this, aStarMode.attack);
		aStar.generateRange(unit.getPosition()[0], unit.getPosition()[1], unit.getRace().getAttackRange(unit.getWeapon().getLeght()),highObstacle);
	}

	public void pathGenerateTile(int x, int y)
	{
		rangeTilemap.SetTile(new Vector3Int(x, y, 0), highObstacle);
	}

	public Map getMap()
	{
		return currentMap;
	}

	public void clearRangeTilemap()
	{
		rangeTilemap.ClearAllTiles();
	}
}
