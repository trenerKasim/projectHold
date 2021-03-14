using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public enum aStarMode {move, attack }
public class AStar
{
	mapManager mapManager;
	aStarMode mode;
    AStarTile[,] tiles;
	AStarTile current;
	List<AStarTile> open = new List<AStarTile>();
	List<AStarTile> close = new List<AStarTile>();
	List<AStarTile> path = new List<AStarTile>();

	public void generateRange(int originX, int originY, int range, Tile tileType)
	{
		range = range * 10;
		close.Clear();
		open.Clear();
		open.Add(tiles[originX, originY]);
		while (open.Capacity > 1)
		{
			open = open.OrderBy(g => g.G).ToList();
			current = open[0];
			if (current.G + 10 < range + 1)
			{
				//Debug.Log(current.G);
				if (current.X - 1 > -1)
				{
					if (!tiles[current.X - 1, current.Y].isInClose)
					{
						if (tiles[current.X - 1, current.Y].walkable)
						{
							if (!tiles[current.X - 1, current.Y].isInOpen || tiles[current.X - 1, current.Y].G > current.G + 10)
							{
								tiles[current.X - 1, current.Y].isInOpen = true;
								tiles[current.X - 1, current.Y].G = current.G + 10;
								open.Add(tiles[current.X - 1, current.Y]);
							}
						}
					}
				}
				if (current.X + 1 < tiles.GetLength(0))
				{
					if (!tiles[current.X + 1, current.Y].isInClose)
					{
						if (tiles[current.X + 1, current.Y].walkable)
						{
							if (!tiles[current.X + 1, current.Y].isInOpen || tiles[current.X + 1, current.Y].G > current.G + 10)
							{
								tiles[current.X + 1, current.Y].isInOpen = true;
								tiles[current.X + 1, current.Y].G = current.G + 10;
								open.Add(tiles[current.X + 1, current.Y]);
							}
						}
					}
				}
				if (current.Y - 1 > -1 && !tiles[current.X, current.Y - 1].isInClose)
				{
					if (!tiles[current.X, current.Y - 1].isInClose)
					{
						if (tiles[current.X, current.Y - 1].walkable)
						{
							if (!tiles[current.X, current.Y - 1].isInOpen || tiles[current.X, current.Y - 1].G > current.G + 10)
							{
								tiles[current.X, current.Y - 1].isInOpen = true;
								tiles[current.X, current.Y - 1].G = current.G + 10;
								open.Add(tiles[current.X, current.Y - 1]);
							}
						}
					}
				}
				if (current.Y + 1 < tiles.GetLength(1))
				{
					if (!tiles[current.X, current.Y + 1].isInClose)
					{
						if (tiles[current.X, current.Y + 1].walkable)
						{
							if (!tiles[current.X, current.Y + 1].isInOpen || tiles[current.X, current.Y + 1].G > current.G + 10)
							{
								tiles[current.X, current.Y + 1].isInOpen = true;
								tiles[current.X, current.Y + 1].G = current.G + 10;
								open.Add(tiles[current.X, current.Y + 1]);
							}
						}
					}
				}
			}
			if (current.G + 15 < range + 1)
			{
				//Debug.Log(current.G);
				if ((current.X - 1 > -1 && current.Y - 1 > -1))
				{
					if (!tiles[current.X - 1, current.Y - 1].isInClose)
					{
						if ((tiles[current.X - 1, current.Y - 1].walkable && tiles[current.X - 1, current.Y].walkable) || (tiles[current.X - 1, current.Y - 1].walkable && tiles[current.X, current.Y - 1].walkable))
						{
							if (!tiles[current.X - 1, current.Y - 1].isInOpen || tiles[current.X - 1, current.Y - 1].G > current.G + 15)
							{
								tiles[current.X - 1, current.Y - 1].isInOpen = true;
								tiles[current.X - 1, current.Y - 1].G = current.G + 15;
								open.Add(tiles[current.X - 1, current.Y - 1]);
							}
						}
					}
				}
				if (current.X + 1 < tiles.GetLength(0) && current.Y - 1 > -1)
				{
					if (!tiles[current.X + 1, current.Y - 1].isInClose)
					{
						if ((tiles[current.X + 1, current.Y - 1].walkable && tiles[current.X + 1, current.Y].walkable) || (tiles[current.X + 1, current.Y - 1].walkable && tiles[current.X, current.Y - 1].walkable))
						{
							if (!tiles[current.X + 1, current.Y - 1].isInOpen || tiles[current.X + 1, current.Y - 1].G > current.G + 15)
							{
								tiles[current.X + 1, current.Y - 1].isInOpen = true;
								tiles[current.X + 1, current.Y - 1].G = current.G + 15;
								open.Add(tiles[current.X + 1, current.Y - 1]);
							}
						}
					}
				}
				if (current.X - 1 > -1 && current.Y + 1 < tiles.GetLength(1))
				{
					if (!tiles[current.X - 1, current.Y + 1].isInClose)
					{
						if ((tiles[current.X - 1, current.Y + 1].walkable && tiles[current.X - 1, current.Y].walkable) || (tiles[current.X - 1, current.Y + 1].walkable && tiles[current.X, current.Y + 1].walkable))
						{
							if (!tiles[current.X - 1, current.Y + 1].isInOpen || tiles[current.X - 1, current.Y + 1].G > current.G + 15)
							{
								tiles[current.X - 1, current.Y + 1].isInOpen = true;
								tiles[current.X - 1, current.Y + 1].G = current.G + 15;
								open.Add(tiles[current.X - 1, current.Y + 1]);
							}
						}
					}
				}
				if (current.X + 1 < tiles.GetLength(0) && current.Y + 1 < tiles.GetLength(1))
				{
					if (!tiles[current.X + 1, current.Y + 1].isInClose)
					{
						if ((tiles[current.X + 1, current.Y + 1].walkable && tiles[current.X + 1, current.Y].walkable) || (tiles[current.X + 1, current.Y + 1].walkable && tiles[current.X, current.Y + 1].walkable))
						{
							if (!tiles[current.X + 1, current.Y + 1].isInOpen || tiles[current.X + 1, current.Y + 1].G > current.G + 15)
							{
								tiles[current.X + 1, current.Y + 1].isInOpen = true;
								tiles[current.X + 1, current.Y + 1].G = current.G + 15;
								open.Add(tiles[current.X + 1, current.Y + 1]);
							}
						}
					}
				}
			}
			open.Remove(current);
			open.TrimExcess();
			current.isInClose = true;
			close.Add(current);
			//Debug.Log(open.Capacity);
		}
		foreach (AStarTile tile in close)
		{
			if(tile.G != 0)
			{
				mapManager.generateRangeTile(tile.X, tile.Y,tileType);
			}
			//Debug.Log("X" + tile.X + "Y" + tile.Y);
		}

	}

	public bool findPath(int originX, int originY, int targetX, int targetY, int range, Unit movingUnit)
	{
		path.Clear();
		path.TrimExcess();
		close.Clear();
		close.TrimExcess();
		open.Clear();
		open.TrimExcess();
		open.Add(tiles[originX, originY]);
		open[0].parent = open[0];
		while (open.Capacity > 1)
		{
			open = open.OrderBy(f => f.F).ToList();
			current = open[0];
			if(current.X == targetX && current.Y == targetY)
			{
				mapManager.clearRangeTilemap();
				if (current.G > range * 10 ||  current.G == 0)
				{
					return false;
				}
				//Debug.Log(current.F);
				AStarTile backtraked = current;
				while(backtraked.parent != backtraked)
				{
					path.Insert(0,backtraked);
					//Debug.Log(backtraked.parent.X + " " + backtraked.parent.Y);
					//mapManager.pathGenerateTile(backtraked.X, backtraked.Y);
					backtraked = backtraked.parent;
				}
				return true;
			}
			if (current.X - 1 > -1)
			{
				if (!tiles[current.X - 1, current.Y].isInClose)
				{
					if (tiles[current.X - 1, current.Y].walkable)
					{
						tiles[current.X - 1, current.Y].setH(targetX, targetY);
						if (!tiles[current.X - 1, current.Y].isInOpen || tiles[current.X - 1, current.Y].G > current.G+ 10)
						{
							tiles[current.X - 1, current.Y].isInOpen = true;
							tiles[current.X - 1, current.Y].G = current.G + 10;
							tiles[current.X - 1, current.Y].parent = current;
							tiles[current.X - 1, current.Y].setF();
							open.Add(tiles[current.X - 1, current.Y]);
						}
						//Debug.Log(tiles[current.X - 1, current.Y].F);
					}
				}
			}
			if (current.X + 1 < tiles.GetLength(0))
			{
				if (!tiles[current.X + 1, current.Y].isInClose)
				{
					if (tiles[current.X + 1, current.Y].walkable)
					{
						tiles[current.X + 1, current.Y].setH(targetX, targetY);
						if (!tiles[current.X + 1, current.Y].isInOpen || tiles[current.X + 1, current.Y].G > current.G + 10)
						{
							tiles[current.X + 1, current.Y].isInOpen = true;
							tiles[current.X + 1, current.Y].G = current.G + 10;
							tiles[current.X + 1, current.Y].parent = current;
							tiles[current.X + 1, current.Y].setF();
							open.Add(tiles[current.X + 1, current.Y]);
						}
					}
				}
			}
			if (current.Y - 1 > -1 && !tiles[current.X, current.Y - 1].isInClose)
			{
				if (!tiles[current.X, current.Y - 1].isInClose)
				{
					if (tiles[current.X, current.Y - 1].walkable)
					{
						tiles[current.X, current.Y - 1].setH(targetX, targetY);
						if (!tiles[current.X, current.Y - 1].isInOpen || tiles[current.X, current.Y - 1].G > current.G + 10)
						{
							tiles[current.X, current.Y - 1].isInOpen = true;
							tiles[current.X, current.Y - 1].G = current.G + 10;
							tiles[current.X, current.Y - 1].parent = current;
							tiles[current.X, current.Y - 1].setF();
							open.Add(tiles[current.X, current.Y - 1]);
						}
					}
				}
			}
			if (current.Y + 1 < tiles.GetLength(1))
			{
				if (!tiles[current.X, current.Y + 1].isInClose)
				{
					if (tiles[current.X, current.Y + 1].walkable)
					{
						tiles[current.X, current.Y + 1].setH(targetX, targetY);
						if (!tiles[current.X, current.Y + 1].isInOpen || tiles[current.X, current.Y + 1].G > current.G + 10)
						{
							tiles[current.X, current.Y + 1].isInOpen = true;
							tiles[current.X, current.Y + 1].G = current.G + 10;
							tiles[current.X, current.Y + 1].parent = current;
							tiles[current.X, current.Y + 1].setF();
							open.Add(tiles[current.X, current.Y + 1]);
						}
					}
				}
			}
			if (current.X - 1 > -1 && current.Y - 1 > -1)
			{
				if (!tiles[current.X - 1, current.Y - 1].isInClose)
				{
					if ((tiles[current.X - 1, current.Y - 1].walkable && tiles[current.X - 1, current.Y].walkable) || (tiles[current.X - 1, current.Y - 1].walkable && tiles[current.X, current.Y - 1].walkable))
					{
						tiles[current.X - 1, current.Y - 1].setH(targetX, targetY);
						if (!tiles[current.X - 1, current.Y - 1].isInOpen || tiles[current.X - 1, current.Y - 1].G > current.G + 15)
						{
							tiles[current.X - 1, current.Y - 1].isInOpen = true;
							tiles[current.X - 1, current.Y - 1].G = current.G + 15;
							tiles[current.X - 1, current.Y - 1].parent = current;
							tiles[current.X - 1, current.Y - 1].setF();
							open.Add(tiles[current.X - 1, current.Y - 1]);
						}
					}
				}
			}
			if (current.X + 1 < tiles.GetLength(0) && current.Y - 1 > -1)
			{
				if (!tiles[current.X + 1, current.Y - 1].isInClose)
				{
					if ((tiles[current.X + 1, current.Y - 1].walkable && tiles[current.X + 1, current.Y].walkable) || (tiles[current.X + 1, current.Y - 1].walkable && tiles[current.X, current.Y - 1].walkable))
					{
						tiles[current.X + 1, current.Y- 1].setH(targetX, targetY);
						if (!tiles[current.X + 1, current.Y - 1].isInOpen || tiles[current.X + 1, current.Y - 1].G > current.G + 15)
						{
							tiles[current.X + 1, current.Y - 1].isInOpen = true;
							tiles[current.X + 1, current.Y - 1].G = current.G + 15;
							tiles[current.X + 1, current.Y - 1].parent = current;
							tiles[current.X + 1, current.Y - 1].setF();
							open.Add(tiles[current.X + 1, current.Y - 1]);
						}
					}
				}
			}
			if (current.X - 1 > -1 && current.Y + 1 < tiles.GetLength(1))
			{
				if (!tiles[current.X - 1, current.Y + 1].isInClose)
				{
					if ((tiles[current.X - 1, current.Y + 1].walkable && tiles[current.X - 1, current.Y].walkable) || (tiles[current.X - 1, current.Y + 1].walkable && tiles[current.X, current.Y + 1].walkable))
					{
						tiles[current.X - 1, current.Y + 1].setH(targetX, targetY);
						if (!tiles[current.X - 1, current.Y + 1].isInOpen || tiles[current.X - 1, current.Y + 1].G > current.G + 15)
						{
							tiles[current.X - 1, current.Y + 1].isInOpen = true;
							tiles[current.X - 1, current.Y + 1].G = current.G + 15;
							tiles[current.X - 1, current.Y + 1].parent = current;
							tiles[current.X - 1, current.Y + 1].setF();
							open.Add(tiles[current.X - 1, current.Y + 1]);
						}
					}
				}
			}
			if (current.X + 1 < tiles.GetLength(0) && current.Y + 1 < tiles.GetLength(1))
			{
				if (!tiles[current.X + 1, current.Y + 1].isInClose)
				{
					if ((tiles[current.X + 1, current.Y + 1].walkable && tiles[current.X + 1, current.Y].walkable) || (tiles[current.X + 1, current.Y + 1].walkable && tiles[current.X, current.Y + 1].walkable))
					{
						tiles[current.X + 1, current.Y + 1].setH(targetX, targetY);
						if (!tiles[current.X + 1, current.Y + 1].isInOpen || tiles[current.X + 1, current.Y + 1].G > current.G + 15)
						{
							tiles[current.X + 1, current.Y + 1].isInOpen = true;
							tiles[current.X + 1, current.Y + 1].G = current.G + 15;
							tiles[current.X + 1, current.Y + 1].parent = current;
							tiles[current.X + 1, current.Y + 1].setF();
							open.Add(tiles[current.X + 1, current.Y + 1]);
						}
					}
				}
			}
			open.Remove(current);
			open.TrimExcess();
			current.isInClose = true;
			close.Add(current);
		}
		return false;
	}

	public List<AStarTile> getPath()
	{
		path.TrimExcess();
		//Debug.Log(path.Capacity);
		return path;
	}

	public AStar(int[] size, mapManager givenMapManager, aStarMode givenMode)
	{
		mapManager = givenMapManager;
		mode = givenMode;
		tiles = new AStarTile[size[0], size[1]];
		for(int x=0;x<size[0];x++)
		{
			for(int y=0;y<size[1];y++)
			{
				if (mode == aStarMode.move)
				{
					if (mapManager.getMap().getTile(x, y).getTileAccess() == access.unoccupaied && mapManager.getMap().getTile(x, y).checkIfOccupied() == false)
					{
						tiles[x, y] = new AStarTile(x, y, true);
					}
					else
					{
						tiles[x, y] = new AStarTile(x, y, false);
					}
				}
				else if (mode == aStarMode.attack)
				{
					if (mapManager.getMap().getTile(x, y).getTileAccess() != access.highObstacle)
					{
						tiles[x, y] = new AStarTile(x, y, true);
					}
					else
					{
						tiles[x, y] = new AStarTile(x, y, false);
					}
				}
			}
		}
	}
}

public class AStarTile
{
	public int X = 0;
	public int Y = 0;

	public int G = 0;
	public int H = 0;
	public int F = 0;
	public AStarTile parent = null;
	public bool walkable = false;
	public bool isInOpen = false;
	public bool isInClose = false;

	public void setH(int targetX, int targetY)
	{
		H = 0;
		int distanceX = Mathf.Abs(X - targetX);
		int distanceY = Mathf.Abs(Y - targetY);
		if (distanceX < distanceY)
		{
			H = distanceX * 15;
			H += (distanceY - distanceX) * 10;
		}
		else if (distanceY < distanceX)
		{
			H = distanceY * 15;
			H += (distanceX - distanceY) * 10;
		}
		else
		{
			H = distanceX * 15;
		}
	}

	public void setF()
	{
		F = G + H;
	}

	public AStarTile(int x, int y, bool givenWalkable)
	{
		X = x;
		Y = y;
		walkable = givenWalkable;
	}
}
