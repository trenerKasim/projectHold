using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

[System.Serializable]
public class topPanelManager : MonoBehaviour
{
	[SerializeField]
	battleManager battleManager;

	[SerializeField]
	TextMeshProUGUI textDisplay;

	[SerializeField]
	Image lightImage;

	[SerializeField]
	Sprite[] lightSprites;
	
    public void initialize()
    {
		battleManager = GetComponent<battleManager>();
		StartCoroutine(clockRefresh());
		setLightImage();
    }

    private IEnumerator clockRefresh()
	{
		while(true)
		{
			textDisplay.text = DateTime.Now.TimeOfDay.Hours+":"+DateTime.Now.TimeOfDay.Minutes;
			//Debug.Log("時");
			yield return new WaitForSeconds(10);
		}
	}

	public void setLightImage()
	{
		switch(battleManager.getMap().getMapLight().ToString())
		{
			case "day" : lightImage.sprite = lightSprites[0]; break;
			case "night": lightImage.sprite = lightSprites[1]; break;
		}
	}
}
