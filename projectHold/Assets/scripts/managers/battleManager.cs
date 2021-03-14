using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

enum gameState { idle, endTurn, endRound, moveChoosing, attackChoosing, action}
public class battleManager : MonoBehaviour
{

	[SerializeField]
	gameState GameState;

	[SerializeField]
	resourceManager resourceManager;
	[SerializeField]
	logManager logManager;
	[SerializeField]
	cameraManager cameraManager;
	[SerializeField]
	mapManager mapManager;
	[SerializeField]
	unitPanelManager unitPanelManager;
	[SerializeField]
	topPanelManager topPanelManager;
	[SerializeField]
	Grid grid;

	[SerializeField]
	bool isChoosingMove = false;

	[SerializeField]
	List<Unit> allUnits = new List<Unit>();
	[SerializeField]
	List<Unit> unitsToGo = new List<Unit>();
	[SerializeField]
	Unit activeUnit;

	public List<Unit> getAllUnits()
	{
		return allUnits;
	}

	public Unit getActiveUnit()
	{
		return activeUnit;
	}

	void Start()
	{
		resourceManager = GetComponent<resourceManager>();
		logManager = GetComponent<logManager>();
		cameraManager = GetComponent<cameraManager>();
		mapManager = GetComponent<mapManager>();
		unitPanelManager = GetComponent<unitPanelManager>();
		topPanelManager = GetComponent<topPanelManager>();
		unitPanelManager.intialize();
		mapManager.generateMap();
		cameraManager.initilize();
		mapManager.avalibityMapReset(false);
		topPanelManager.initialize();
		startBattle();
	}

	public void startBattle()
	{
		allUnits = resourceManager.getActiveRooster();
		allUnits.AddRange(resourceManager.getEnemyRooster());
		foreach (Unit unit in allUnits)
		{
			unit.setPawn(resourceManager.getPawnPrefab());
			unit.setPawnPosition(grid.CellToWorld(new Vector3Int(unit.getPosition()[0], unit.getPosition()[1], 0)));
			unit.rollInitiative();
			mapManager.placeUnit(unit.getPosition());
		}
		mapManager.avalibityMapReset(false);
		unitsToGo = allUnits;
		GameState = gameState.endTurn;
	}

	void Update()
	{
		switch(GameState)
		{
			case gameState.idle: break;
			case gameState.endTurn: GameState = gameState.idle; newTurn(); break;
			case gameState.endRound: GameState = gameState.idle;  newRund(); break;
			case gameState.moveChoosing: break;
			case gameState.attackChoosing: break;
			case gameState.action: break;
		}
	}

	void newTurn()
	{
		//Debug.Log(unitsToGo.Capacity);
		if(unitsToGo.Capacity < 2)
		{
			//Debug.Log("いち");
			GameState = gameState.endRound;
			return;
		}
		unitsToGo = unitsToGo.OrderBy(i => i.getInitiative()).ToList();
		activeUnit = unitsToGo[0];
		unitsToGo.Remove(activeUnit);
		unitsToGo.TrimExcess();
		activeUnit.resetActions();
		if (activeUnit.getSide() == 1)
		{
			logManager.logWrite("Enemy Turn!");
			GameState = gameState.endTurn;
		}
		else
		{
			unitPanelManager.updateDisplay();
		}
	}

	void newRund()
	{
		unitsToGo = allUnits;
		GameState = gameState.endTurn;
	}

	public void moveAction()
	{
		if (GameState == gameState.idle)
		{
			if (activeUnit.getActions() == 0)
			{
				logManager.debugLogWrite("Out of actions!");
				return;
			}
			mapManager.moveRangeGenerate(activeUnit);
			GameState = gameState.moveChoosing;
			switchChoosingMode();
		}
	}

	public void attackAction()
	{
		if (GameState == gameState.idle)
		{
			if (activeUnit.getActions() == 0)
			{
				logManager.debugLogWrite("Out of actions!");
				return;
			}
			mapManager.attackRangeGenerate(activeUnit);
			GameState = gameState.attackChoosing;
			switchChoosingMode();
		}
	}

	public void moving()
	{
		if (Input.GetButtonDown("leftClick") && GameState == gameState.moveChoosing)
		{
			GameState = gameState.action;
			switchChoosingMode();
			Vector3Int pos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			//Debug.Log(pos.x+","+pos.y+","+pos.z);
			AStar aStar = new AStar(mapManager.getMap().getSize(), mapManager, aStarMode.move);
			if (aStar.findPath(activeUnit.getPosition()[0], activeUnit.getPosition()[1], pos.x, pos.y, activeUnit.getSpeed(), activeUnit))
			{
				StartCoroutine(move(aStar, pos, resourceManager.getUnitMovementSpeed()));
			}
		}
		else
		{
			mapManager.clearRangeTilemap();
		}
	}

	public void attacking()
	{
		if(Input.GetButtonDown("leftClick") && GameState == gameState.attackChoosing)
		{
			//do attack stuff
		}
		mapManager.clearRangeTilemap();
		GameState = gameState.idle;
	}

	public void endTurnButton()
	{
		GameState = gameState.endTurn;
	}

	public void switchChoosingMode()
	{
		isChoosingMove = !isChoosingMove;
	}

	public Unit getUnit(int id)
	{
		return allUnits[id];
	}

	public Map getMap()
	{
		return mapManager.getMap();
	}

	IEnumerator move(AStar aStar, Vector3Int pos, float time)
	{
		mapManager.getMap().getTile(activeUnit.getPosition()[0], activeUnit.getPosition()[1]).setIsOccupied(false);
		List<AStarTile> path = aStar.getPath();
		foreach (AStarTile tile in path)
		{
			yield return new WaitForSeconds(resourceManager.getUnitMovementSpeed());
			activeUnit.setPosition(tile.X, tile.Y);
			activeUnit.setPawnPosition(grid.CellToWorld(new Vector3Int(activeUnit.getPosition()[0], activeUnit.getPosition()[1], 0)));
		}
		Debug.Log("今");
		activeUnit.setPosition(pos.x, pos.y);
		mapManager.placeUnit(activeUnit.getPosition());
		activeUnit.setPawnPosition(grid.CellToWorld(new Vector3Int(activeUnit.getPosition()[0], activeUnit.getPosition()[1], 0)));
		activeUnit.useAction();
		unitPanelManager.actionRunesUpdate();
		mapManager.avalibityMapReset(false);
		mapManager.placeUnit(activeUnit.getPosition());
		GameState = gameState.idle;
	}
}
