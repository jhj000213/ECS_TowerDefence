using Unity.Entities;

public struct TeamCD : IComponentData
{
    public Team team;

    public bool IsEqualTeam(in Team otherTeam)
    {
        bool ret = team == otherTeam;
        return ret;
    }
    public bool IsEqualTeam(in TeamCD otherTeamCD)
    {
        bool ret = team == otherTeamCD.team;
        return ret;
    }
    public bool IsEqualTeam(in RefRO<TeamCD> otherTeamCD)
    {
        bool ret = team == otherTeamCD.ValueRO.team;
        return ret;
    }
}

public enum Team
{
    NONE,
    ENEMY,
    TOWER
}