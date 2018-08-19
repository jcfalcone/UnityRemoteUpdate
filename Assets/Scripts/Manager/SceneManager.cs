using UnityEngine;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class SceneManager : MonoBehaviour 
{
	[SerializeField]
	GameObject cubePrefab;
	
	[SerializeField]
	Vector3Scriptable CubeSpeed;
	
	[SerializeField]
	ColorScriptable CubeColor;
	
	[SerializeField]
	Vector3Scriptable TempCubeSpeed;
	
	[SerializeField]
	ColorScriptable TempCubeColor;

	[SerializeField]
	GameEvent OnColorChange;

	[SerializeField]
	GameEvent OnSpeedChange;

	EntityManager manager;

	int cubeCount = 0;
	// Use this for initialization
	void Start () 
	{
	}

	public void CheckData()
	{
		if(this.TempCubeColor.value != this.CubeColor.value)
		{
			this.CubeColor.value = this.TempCubeColor.value;
			this.OnColorChange.Raise();
		}

		if(this.TempCubeSpeed.value != this.CubeSpeed.value)
		{
			this.CubeSpeed.value = this.TempCubeSpeed.value;
			this.OnSpeedChange.Raise();
		}
	}

	void CreateCubes(int _amount)
	{
		NativeArray<Entity> entities = new NativeArray<Entity>(_amount, Allocator.Temp);
		this.manager.Instantiate(this.cubePrefab, entities);

		for(int count = 0; count < _amount; count++)
		{
			float3 position;
			position.x = Random.Range(-10f, 10f);
			position.y = Random.Range(-10f, 10f);
			position.z = Random.Range(0f, 150f);

			this.manager.SetComponentData(entities[count], new Position { Value = position});
			this.manager.SetComponentData(entities[count], new Rotation { Value = quaternion.identity});
			this.manager.SetComponentData(entities[count], new RotationSpeed { Value = 1f});
		}

		entities.Dispose();
		cubeCount += _amount;
	}

	void CreateCubes(int _amount, float3 _position)
	{
		NativeArray<Entity> entities = new NativeArray<Entity>(_amount, Allocator.Temp);
		this.manager.Instantiate(this.cubePrefab, entities);

		for(int count = 0; count < _amount; count++)
		{
			this.manager.SetComponentData(entities[count], new Position { Value = _position});
			this.manager.SetComponentData(entities[count], new Rotation { Value = quaternion.identity});
			this.manager.SetComponentData(entities[count], new RotationSpeed { Value = 1f});
		}

		entities.Dispose();
		cubeCount += _amount;

		Debug.Log(cubeCount);
	}
}
