using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class RotationSystem : JobComponentSystem
{

	[Unity.Burst.BurstCompile]
	struct RotationJob : IJobProcessComponentData<Rotation, RotationSpeed>
	{
		public float deltaTime;
		public float speed;

		public void Execute(ref Rotation _rotation, ref RotationSpeed _rotationSpeed)
		{
			_rotationSpeed.Value = this.speed;
			_rotation.Value = math.mul(math.normalize(_rotation.Value), quaternion.axisAngle(math.up(), _rotationSpeed.Value * this.deltaTime));
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDeps)
	{
		RotationJob rotationJob = new RotationJob
		{
			deltaTime = Time.deltaTime,
			speed = 1f
		};

		JobHandle rotationHandle = rotationJob.Schedule(this, 64, inputDeps);

		return rotationHandle;
	}
}
