using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

//[WithNone(typeof(FreezePositionCD))]
//[WithAll(typeof(TagX))]
public partial struct MoveTowardJob : IJobEntity
{
    public float deltaTime;

    public void Execute(ref MoveCD moveCD, in MovePathCD movePathCD, ref LocalTransform transform)
    {
        float3 targetPosition = movePathCD.NowTargetPosition;

        float3 dis = targetPosition - transform.Position;
        if(math.length(dis) < 0.01f)
        {
            return;
        }
        float3 dt = math.normalize(dis) * moveCD.speed * deltaTime;
        transform.Position = transform.Position + dt;
    }
}
