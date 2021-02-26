using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class logManager : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI log;

	public void logClear()
	{
		log.text = "";
	}

	public void logWrite(string text)
	{
		log.text = text + "\n" + log.text;
	}

	public void debugLogWrite(string text)
	{
		log.text = "<color=#000000>" + text + "</color>\n" + log.text;
	}
}
