using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct MoveToTargetJob : IJobEntity
{
    public float deltaTime;
    public void Execute(in MoveCD moveCD, in MoveTargetCD targetCD, ref LocalTransform transform)
    {
        float3 targetPosition = targetCD.targetPosition;

        float3 dis = targetPosition - transform.Position;
        if (math.length(dis) < 0.01f)
        {
            return;
        }

        float3 dt = math.normalize(dis) * moveCD.speed * deltaTime;
        transform.Position = transform.Position + dt;
    }
}
