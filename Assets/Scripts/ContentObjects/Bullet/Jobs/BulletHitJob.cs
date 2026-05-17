using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

[WithAll(typeof(Tags.Bullet), typeof(StateTags.IsArrived))]
public partial struct BulletHitJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ecb;

    [ReadOnly] public ComponentLookup<TeamCD> teamLookup;
    [ReadOnly] public EntityStorageInfoLookup entityStorageLookup;
    [ReadOnly] public ComponentLookup<StateTags.IsAlive> isAliveLookup;
    
    [NativeDisableParallelForRestriction]
    public ComponentLookup<HPCD> hpLookup;

    public void Execute(Entity entity, [EntityIndexInQuery] int sortKey,
        ref TargetEntityCD targetCD, in TeamCD myTeamCD, in BulletObjectCD bulletObjectCD)
    {
        if (isAliveLookup.IsComponentEnabled(entity) == false)
            return;

        Entity target = targetCD.targetEntity;
        if (entityStorageLookup.Exists(target) == false)
        {
            JDebugLogger.Log("Target is already dead");

            DestroyBullet(sortKey, entity);
            return;
        }

        if (myTeamCD.IsEqualTeam(teamLookup.GetRefRO(target)))
            return;

        if (hpLookup.EntityExists(target) == false)
            return;

        var hpCD = hpLookup.GetRefRW(target);

        hpCD.ValueRW.SetDamage(bulletObjectCD.damage);

        //JDebugLogger.Log(entity.ToString());
        DestroyBullet(sortKey, entity);
        return;
    }

    void DestroyBullet(int sortKey, Entity entity)
    {
        ecb.SetComponentEnabled<StateTags.IsAlive>(sortKey, entity, false);
        ecb.DestroyEntity(sortKey, entity);
    }
}
