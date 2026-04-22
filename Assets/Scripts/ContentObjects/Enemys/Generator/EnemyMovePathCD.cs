using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct EnemyMovePathCD : IComponentData
{
    public FixedList64Bytes<float3> positions;
    public int maxIndex;
}
