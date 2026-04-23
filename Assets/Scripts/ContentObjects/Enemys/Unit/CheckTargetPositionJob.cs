using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct CheckTargetPositionJob : IJobEntity
{
    public void Execute(in LocalTransform transform, ref MovePathCD movePathCD)
    {
        float3 nowPosition = transform.Position;

        if (movePathCD.IsArrived(nowPosition) == true)
        {
            movePathCD.SetNext();
            JDebugLogger.Log("target", movePathCD.NowTargetPosition);
        }
    }
}
