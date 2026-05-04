using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public static class MoveUtils
{
    public static bool IsArrive(this in float3 nowPosition, in float3 targetPosition)
    {
        float3 dis = targetPosition - nowPosition;
        if (math.length(dis) < 0.08f)
        {
            return true;
        }

        return false;
    }
}
