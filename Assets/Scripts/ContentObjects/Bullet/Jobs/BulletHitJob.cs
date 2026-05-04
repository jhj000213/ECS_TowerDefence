using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;

[WithAll(typeof(Tags.Bullet), typeof(StateTags.IsArrived), typeof(StateTags.IsAlive))]
public partial struct BulletHitJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ecb;

    [ReadOnly] public ComponentLookup<TeamCD> teamLookup;
    [ReadOnly] public EntityStorageInfoLookup entityStorageLookup;

    [NativeDisableParallelForRestriction]
    public ComponentLookup<HPCD> hpLookup;

    public void Execute(Entity entity, [EntityIndexInQuery] int sortKey,ref TargetEntityCD targetCD, in TeamCD myTeamCD, in BulletObjectCD bulletObjectCD)
    {
        Entity target = targetCD.targetEntity;
        if (entityStorageLookup.Exists(target) == false)
            return;

        if (myTeamCD.IsEqualTeam(teamLookup.GetRefRO(target)))
            return;

        if (hpLookup.EntityExists(target) == false)
            return;

        var hpCD = hpLookup.GetRefRW(target);

        hpCD.ValueRW.SetDamage(bulletObjectCD.damage);

        ecb.DestroyEntity(sortKey, entity);
        ecb.SetComponentEnabled<StateTags.IsAlive>(sortKey, entity, false);
    }
}
