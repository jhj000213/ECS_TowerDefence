//using System.ComponentModel;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[WithAll(typeof(StateTags.AttackReady))]
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
        in TowerCD towerCD, ref AttackCD attackCD, in TeamCD myTeamCD)
    {
        FindTarget(transformLookup[entity], myTeamCD);

        if (existTarget == false)
            return;

        ecb.RemoveComponent<StateTags.AttackReady>(sortKey, entity);

        Entity newEntity = ecb.Instantiate(sortKey, attackCD.attackPrefab);
        ecb.AddComponent(sortKey, newEntity, new MoveTargetCD()
        {
            targetPosition = transformLookup[target].Position,
        });
        ecb.AddComponent(sortKey, newEntity, new Parent { Value = attackObjParent });
        ecb.SetComponent(sortKey, newEntity, LocalTransform.FromPosition(transformLookup[entity].Position + towerCD.bulletStartPosition));

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

            if (myTeamCD.team == enemyTeam.team)
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
