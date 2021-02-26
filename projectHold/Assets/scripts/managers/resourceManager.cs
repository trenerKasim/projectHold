using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceManager : MonoBehaviour
{
	[SerializeField]
	List<Unit> activeRooster = new List<Unit>();

	[SerializeField]
	GameObject pawnPrefab;

	public List<Unit> getActiveRooster()
	{
		return activeRooster;
	}

	public GameObject getPawnPrefab()
	{
		return pawnPrefab;
	}
}
