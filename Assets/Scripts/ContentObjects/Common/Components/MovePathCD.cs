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
        nowIndex += 1;
        if (nowIndex >= positions.Length)
        {
            nowIndex = 0;
        }
    }
}
