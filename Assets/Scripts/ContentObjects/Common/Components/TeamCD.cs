using Unity.Entities;

public struct TeamCD : IComponentData
{
    public Team team;
}

public enum Team
{
    NONE,
    ENEMY,
    TOWER
}