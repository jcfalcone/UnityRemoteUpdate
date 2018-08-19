using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RotationWeb
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }
}

[System.Serializable]
public class ColorWeb
{
    public float R { get; set; }
    public float G { get; set; }
    public float B { get; set; }
    public float A { get; set; }
}

[System.Serializable]
public struct NetworkData
{
    public List<float> Rotation;
    public List<float> Color;
}

public class NetworkManager : MonoBehaviour {

	[SerializeField]
	string TargetUrl;

	[SerializeField]
	ColorScriptable CubeColor;

	[SerializeField]
	Vector3Scriptable CubeSpeed;

	[SerializeField]
	StringScriptable LastError;

	[SerializeField]
	StringScriptable AverageTime;

	[SerializeField]
	GameEvent OnNetworkError;

	[SerializeField]
	GameEvent OnNetworkCompleto;

	WaitForEndOfFrame endOfFrame = new WaitForEndOfFrame();

	WaitForSeconds errorWaitTime = new WaitForSeconds(5);

	WaitForSeconds networkWaitTime = new WaitForSeconds(0.1f);

	float averageTime;
	float countDownload;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine(LoopUpdate());
	}
	
	// Update is called once per frame
	IEnumerator LoopUpdate () 
	{
		float startTime = Time.time;
		WWW file = new WWW(this.TargetUrl);

		while(!file.isDone)
		{
			yield return this.endOfFrame;
		}

		if(file.error != null && file.error.Length > 0)
		{
			this.LastError.value = file.error;
			this.OnNetworkError.Raise();

			this.countDownload++;
			this.averageTime += (Time.time - startTime) / this.countDownload;

			this.AverageTime.value = string.Format("Average Download Time : {0} in {1}", this.averageTime, this.countDownload);
			yield return this.errorWaitTime;

			StartCoroutine(this.LoopUpdate());

			yield break;
		}

		if(file.text.Length <= 0)
		{
			this.LastError.value = "Empty File";
			this.OnNetworkError.Raise();

			this.countDownload++;
			this.averageTime += (Time.time - startTime) / this.countDownload;

			this.AverageTime.value = string.Format("Average Download Time : {0} in {1}", this.averageTime, this.countDownload);
			yield return this.errorWaitTime;

			StartCoroutine(this.LoopUpdate());

			yield break;
		}

		NetworkData data = JsonUtility.FromJson<NetworkData>(file.text);

		this.CubeSpeed.value = new Vector3(data.Rotation[0], data.Rotation[1], data.Rotation[2]);
		Color32 tempColor = this.CubeColor.value;
		tempColor.r = (byte)data.Color[0];
		tempColor.g = (byte)data.Color[1];
		tempColor.b = (byte)data.Color[2];
		tempColor.a = (byte)data.Color[3];

		this.CubeColor.value = tempColor;

		this.OnNetworkCompleto.Raise();

		this.countDownload++;
		this.averageTime += (Time.time - startTime) / this.countDownload;

		this.AverageTime.value = string.Format("Average Download Time : {0} in {1}", this.averageTime, this.countDownload);
		
		yield return this.networkWaitTime;

		StartCoroutine(LoopUpdate());
	}
}
