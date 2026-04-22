using Unity.Entities;

public struct EnemyMoveCD : IComponentData
{
    public float speed;
    public int pathIndex;
}
