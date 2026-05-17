using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public static class MoveUtils
{
    public static bool IsNearby(this in float3 nowPosition, in float3 targetPosition, float distance = 0.1f)
    {
        float3 dis = targetPosition - nowPosition;
        if (math.length(dis) < distance)
        {
            return true;
        }

        return false;
    }
}
