using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeObj : MonoBehaviour 
{
	[SerializeField]
	[Range(0f, 10f)]
	float speedLerp;

	[SerializeField]
	Material material;
	
	[SerializeField]
	Vector3Scriptable CubeSpeed;
	
	[SerializeField]
	ColorScriptable CubeColor;

	Vector3 currSpeed;

	bool isLerpingColor = false;
	bool isLerpingSpeed = false;

	// Use this for initialization
	void Start () 
	{
		this.CubeSpeed.value = Vector3.zero;
		this.CubeColor.value = Color.white;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 rotation = Vector3.up * this.currSpeed.y;
		rotation += Vector3.right * this.currSpeed.x;
		rotation += Vector3.forward * this.currSpeed.z;

		transform.Rotate(rotation * Time.deltaTime);

		if(this.isLerpingColor)
		{
			this.material.color = Color.Lerp(this.material.color, this.CubeColor.value, Time.deltaTime * this.speedLerp);

			if(this.material.color ==  this.CubeColor.value)
			{
				this.isLerpingColor = false;
			}
		}

		if(this.isLerpingSpeed)
		{
			this.currSpeed = Vector3.Lerp(this.currSpeed, this.CubeSpeed.value, Time.deltaTime * this.speedLerp);

			if(this.currSpeed.magnitude == this.CubeSpeed.value.magnitude)
			{
				this.isLerpingSpeed = false;
			}
		}
	}

	public void UpdateMaterial()
	{
		this.isLerpingColor = true;
	}

	public void UpdateSpeed()
	{
		this.isLerpingSpeed = true;
	}
}
