using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AverageTimeController : MonoBehaviour 
{

	[SerializeField]
	TextMeshProUGUI label;

	[SerializeField]
	StringScriptable AverageTime;

	
	public void UpdateUI()
	{
		this.label.text = this.AverageTime.value;
	}
}
