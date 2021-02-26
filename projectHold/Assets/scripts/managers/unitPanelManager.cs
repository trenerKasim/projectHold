using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class unitPanelManager : MonoBehaviour
{
	[SerializeField]
	battleManager battleManager;

	[SerializeField]
	TextMeshProUGUI displayedName;
	[SerializeField]
	Image displayedPortrait;

	[SerializeField]
	Image[] actionRunes = new Image[2];

	public void intialize()
    {
		battleManager = GetComponent<battleManager>();
    }

	public void updateDisplay()
	{
		displayedName.text = battleManager.getActiveUnit().getName();
		actionRunesUpdate();
	}

    public void actionRunesUpdate()
    {
		switch (battleManager.getActiveUnit().getActions())
		{
			case 0: actionRunes[0].color = Color.red; actionRunes[1].color = Color.red; break;
			case 1: actionRunes[0].color = Color.green; actionRunes[1].color = Color.red; break;
			case 2: actionRunes[0].color = Color.green; actionRunes[1].color = Color.green; break;
		}
	}
}
