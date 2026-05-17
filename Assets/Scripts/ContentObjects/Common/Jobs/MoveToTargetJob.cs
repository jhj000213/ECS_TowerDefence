using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[WithNone(typeof(StateTags.IsArrived))]
[WithAll(typeof(StateTags.IsAlive))]
public partial struct MoveToTargetJob : IJobEntity
{
    public float deltaTime;
    public EntityCommandBuffer.ParallelWriter beginEcb;

    public void Execute(Entity entity, [EntityIndexInQuery] int sortKey, 
        in MoveCD moveCD, in MoveTargetCD targetCD, ref LocalTransform transform)
    {
        float3 targetPosition = targetCD.targetPosition;
        float3 dis = targetPosition - transform.Position;
        bool isArrived = transform.Position.IsNearby(targetPosition);

        if (isArrived == true)
        {
            beginEcb.AddComponent<StateTags.IsArrived>(sortKey, entity);
            return;
        }

        float3 dt = math.normalize(dis) * moveCD.speed * deltaTime;
        transform.Position = transform.Position + dt;
    }
}
