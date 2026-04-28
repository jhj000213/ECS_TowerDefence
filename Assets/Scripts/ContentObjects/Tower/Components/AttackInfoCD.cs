using Unity.Entities;

public struct AttackInfoCD : IComponentData
{
    float damage;
    float arange;
    float delayTime;
    float elapsedTime;

    public AttackInfoCD(float dmg, float attackArange, float attackDelayTime)
    {
        damage = dmg;
        arange = attackArange;
        delayTime = attackDelayTime;
        elapsedTime = 0;
    }
}
