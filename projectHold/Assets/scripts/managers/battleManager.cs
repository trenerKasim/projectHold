using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

enum gameState { idle, endTurn, endRound}
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
		foreach (Unit unit in allUnits)
		{
			unit.setPawn(resourceManager.getPawnPrefab());
			unit.setPawnPosition(grid.CellToWorld(new Vector3Int(unit.getPosition()[0], unit.getPosition()[1], 0)));
			unit.setWeaponSprite();
			unit.setArmorSprite();
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
		unitPanelManager.updateDisplay();
	}

	void newRund()
	{
		unitsToGo = allUnits;
		GameState = gameState.endTurn;
	}

	public void moveAction()
	{
		if(activeUnit.getActions() == 0)
		{
			logManager.debugLogWrite("Out of actions!");
			return;
		}
		mapManager.moveRangeGenerate(activeUnit);
		switchChoosingMode();
	}

	public void moving()
	{
		if (Input.GetButtonDown("leftClick") && isChoosingMove)
		{
			switchChoosingMode();
			Vector3Int pos = grid.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition));
			//Debug.Log(pos.x+","+pos.y+","+pos.z);
			AStar aStar = new AStar(mapManager.getMap().getSize(), mapManager);
			if (aStar.findPath(activeUnit.getPosition()[0], activeUnit.getPosition()[1], pos.x, pos.y, activeUnit.getSpeed()))
			{
				mapManager.getMap().getTile(activeUnit.getPosition()[0], activeUnit.getPosition()[1]).setIsOccupied(false);
				activeUnit.setPosition(pos.x, pos.y);
				mapManager.placeUnit(activeUnit.getPosition());
				activeUnit.setPawnPosition(grid.CellToWorld(new Vector3Int(activeUnit.getPosition()[0], activeUnit.getPosition()[1], 0)));
				activeUnit.useAction();
				unitPanelManager.actionRunesUpdate();
				mapManager.avalibityMapReset(false);
				mapManager.placeUnit(activeUnit.getPosition());
			}
		}
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
}
