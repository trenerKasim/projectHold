using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class tilmapClick : MonoBehaviour
{
	[SerializeField]
	battleManager battleManager;
    public void MouseDown()
	{
		battleManager.moving();
	}
}
