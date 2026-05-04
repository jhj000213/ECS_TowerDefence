//using System.ComponentModel;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public partial struct AttackJob : IJobEntity
{
    public EntityCommandBuffer.ParallelWriter ecb;
    public Entity attackObjParent;

    [ReadOnly] public NativeArray<Entity> enemyEntities;
    [ReadOnly] public ComponentLookup<TeamCD> teamLookup;
    [ReadOnly] public ComponentLookup<LocalTransform> transformLookup;

    bool existTarget;
    Entity target;
    

    public void Execute(Entity entity, [EntityIndexInQuery] int sortKey,
        in TowerCD towerCD, ref AttackCD attackCD, in TeamCD myTeamCD, EnabledRefRW<StateTags.AttackReady> attackReady)
    {
        FindTarget(transformLookup[entity], myTeamCD);

        if (existTarget == false)
            return;

        if (attackCD.IsCanAttack() == false)
            return;

        attackReady.ValueRW = false;
        //ecb.RemoveComponent<StateTags.AttackReady>(sortKey, entity);

        Entity newEntity = ecb.Instantiate(sortKey, attackCD.attackPrefab);
        ecb.AddComponent(sortKey, newEntity, new MoveTargetCD()
        {
            targetPosition = transformLookup[target].Position,
        });
        ecb.AddComponent(sortKey, newEntity, new Parent { Value = attackObjParent });
        ecb.SetComponent(sortKey, newEntity, LocalTransform.FromPosition(transformLookup[entity].Position + towerCD.bulletStartPosition));
        ecb.AddComponent(sortKey, newEntity, new TargetEntityCD()
        {
            targetEntity = target
        });
        ecb.AddComponent(sortKey, newEntity, new BulletObjectCD()
        {
            damage = attackCD.damage
        });
        ecb.AddComponent(sortKey, newEntity, new TeamCD()
        {
            team = myTeamCD.team
        });
        ecb.AddComponent(sortKey, newEntity, new StateTags.IsArrived());
        ecb.SetComponentEnabled<StateTags.IsArrived>(sortKey, newEntity, false);

        attackCD.OnAttack();
    }

    void FindTarget(in LocalTransform transform, in TeamCD myTeamCD)
    {
        existTarget = false;
        target = Entity.Null;

        float closestDistanceSq = float.MaxValue;

        for (int i = 0; i < enemyEntities.Length; i++)
        {
            Entity enemy = enemyEntities[i];
            LocalTransform enemyTransform = transformLookup[enemy];
            TeamCD enemyTeam = teamLookup[enemy];

            if (myTeamCD.IsEqualTeam(enemyTeam))
                continue;

            // 거리의 제곱(math.distancesq)을 사용해 연산 속도 최적화 (루트 연산 방지)
            float distSq = math.distancesq(transform.Position, enemyTransform.Position);

            if (distSq < closestDistanceSq)
            {
                closestDistanceSq = distSq;
                target = enemy;
                existTarget = true;
            }
        }
    }
}
