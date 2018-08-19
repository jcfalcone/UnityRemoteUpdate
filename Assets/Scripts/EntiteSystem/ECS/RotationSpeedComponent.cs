using System;
using Unity.Entities;

[Serializable]
public struct RotationSpeed : IComponentData
{
	public float Value; 

	public RotationSpeed(float _value)
	{
		this.Value = _value;
	}
}

public class RotationSpeedComponent : ComponentDataWrapper<RotationSpeed> { }