using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct MovePathCD : IComponentData
{
    FixedList64Bytes<float3> positions;
    int nowIndex;

    public MovePathCD(FixedList64Bytes<float3> positionList)
    {
        positions = positionList;
        nowIndex = 0;
        JDebugLogger.Log("init");
    }

    public float3 this[int index]
    {
        get
        {
            if (index >= positions.Length)
            {

                return positions[0];
            }
            return positions[index];
        }
    }

    public float3 NowTargetPosition
    {
        get { return positions[nowIndex]; }
    }

    public FixedList64Bytes<float3> PositionList
    {
        get { return positions; }
    }


    public bool IsArrived(float3 position, float checkDistance = 0.1f)
    {
        bool ret = math.distance(position, NowTargetPosition) <= checkDistance;

        return ret;
    }

    public void SetNext()
    {
        JDebugLogger.Log("index1", nowIndex);
        nowIndex += 1;
        if (nowIndex >= positions.Length)
        {
            nowIndex = 0;
            JDebugLogger.Log("index2", nowIndex);
        }
        JDebugLogger.Log("1", positions[0]);
        JDebugLogger.Log("2", positions[1]);
        JDebugLogger.Log("3", positions[2]);
        JDebugLogger.Log("4", positions[3]);
    }
}
