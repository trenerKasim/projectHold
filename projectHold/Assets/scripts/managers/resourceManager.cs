using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceManager : MonoBehaviour
{
	[Header("GameOptions")]
	[SerializeField][Range(2f, 10f)]
	float cameraSpeed;
	[SerializeField][Range(0.3f,1.2f)]
	float unitMovementSpeed;
	[Header("GameData")]
	[SerializeField]
	List<Unit> activeRooster = new List<Unit>();

	[SerializeField]
	List<Unit> enemyRooster = new List<Unit>();

	[SerializeField]
	GameObject pawnPrefab;

	public void setCameraSpeed(float value) {
		cameraSpeed = value;
	}

	public float getCameraSpeed() {
		return cameraSpeed;
	}

	public void updateUnitMovementSpeed(System.Single value) {
		unitMovementSpeed = (-value) / 10f;
	}

	public float getUnitMovementSpeed() {
		return unitMovementSpeed;
	}

	public List<Unit> getActiveRooster()
	{
		return activeRooster;
	}

	public List<Unit> getEnemyRooster()
	{
		return enemyRooster;
	}

	public GameObject getPawnPrefab()
	{
		return pawnPrefab;
	}
}
