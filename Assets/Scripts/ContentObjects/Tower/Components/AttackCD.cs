using Unity.Entities;

public struct AttackCD : IComponentData
{
    public float damage;
    public float arange;
    public float delayTime;
    public float elapsedTime;
    public Entity attackPrefab;

    public AttackCD(float dmg, float attackArange, float attackDelayTime, Entity attackEffectPrefab)
    {
        damage = dmg;
        arange = attackArange;
        delayTime = attackDelayTime;
        elapsedTime = 0;
        attackPrefab = attackEffectPrefab;
    }

    public bool IsCanAttack()
    {
        bool ret = elapsedTime >= delayTime;
        return ret;
    }

    public void OnAttack()
    {
        elapsedTime = 0;
    }
}