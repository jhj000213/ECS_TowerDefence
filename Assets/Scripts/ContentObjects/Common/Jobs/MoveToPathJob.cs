using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

//[WithNone(typeof(FreezePositionCD))]
[WithAll(typeof(StateTags.IsAlive))]
public partial struct MoveToPathJob : IJobEntity
{
    public float deltaTime;

    public void Execute(ref MoveCD moveCD, in MovePathCD movePathCD, ref LocalTransform transform)
    {
        float3 targetPosition = movePathCD.NowTargetPosition;
        float3 dis = targetPosition - transform.Position;
        bool isArrived = transform.Position.IsArrive(targetPosition);

        if (isArrived == true)
            return;

        float3 dt = math.normalize(dis) * moveCD.speed * deltaTime;
        transform.Position = transform.Position + dt;
    }
}
