using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(MoveSystem))]
[UpdateBefore(typeof(EndSimulationEntityCommandBufferSystem))]
partial struct BulletSystem : ISystem
{
    private NativeArray<Entity> targetEntities;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {

    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {

        //var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        //EntityCommandBuffer ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
       
        var endEcbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        EntityCommandBuffer jobEcb = endEcbSingleton.CreateCommandBuffer(state.WorldUnmanaged);


        BulletHitJob hitJob = new BulletHitJob()
        {
            ecb = jobEcb.AsParallelWriter(),

            teamLookup = SystemAPI.GetComponentLookup<TeamCD>(true),
            hpLookup = SystemAPI.GetComponentLookup<HPCD>(),
            isAliveLookup = SystemAPI.GetComponentLookup<StateTags.IsAlive>(true),
            entityStorageLookup = state.GetEntityStorageInfoLookup(),
        };


        JobHandle hitHandle = hitJob.ScheduleParallel(state.Dependency);

        state.Dependency = hitHandle;
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {

    }
}