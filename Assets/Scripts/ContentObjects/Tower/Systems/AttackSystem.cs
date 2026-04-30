using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

partial struct AttackSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (attackCD, entity) in SystemAPI.Query<RefRW<AttackCD>>().WithEntityAccess())
        {
            attackCD.ValueRW.elapsedTime += SystemAPI.Time.DeltaTime;
            if (attackCD.ValueRO.IsCanAttack())
            {
                ecb.AddComponent(entity, new StateTags.AttackReady());
            }
        }

        var query = SystemAPI.QueryBuilder().WithAll<TeamCD>().Build();
        // 2. NativeArray로 변환 (할당량 주의: TempJob 사용)
        var targetEntities = query.ToEntityArray(Allocator.TempJob);

        var spawnerEntity = SystemAPI.GetSingletonEntity<AttackObjectPrefabsCD>();
        var attackObjPrefabCD = SystemAPI.GetComponentRO<AttackObjectPrefabsCD>(spawnerEntity).ValueRO.attackObjectParent;

        AttackJob attackJob = new AttackJob()
        {
            ecb = ecb.AsParallelWriter(),
            attackObjParent = attackObjPrefabCD,

            enemyEntities = targetEntities,
            transformLookup = SystemAPI.GetComponentLookup<LocalTransform>(true),
            teamLookup = SystemAPI.GetComponentLookup<TeamCD>(true)
        };


        JobHandle attackHandle = attackJob.ScheduleParallel(state.Dependency);

        state.Dependency = attackHandle;
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
