using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NetworkErrorControl : MonoBehaviour 
{
	[SerializeField]
	TextMeshProUGUI label;

	[SerializeField]
	StringScriptable NetworkError;

	
	public void UpdateUI()
	{
		this.label.text = this.NetworkError.value;
	}
}
