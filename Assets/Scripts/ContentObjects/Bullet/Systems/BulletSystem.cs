using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateAfter(typeof(MoveSystem))]
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

        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        EntityCommandBuffer ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (moveTarget, transform, entity) in SystemAPI.Query<RefRO<MoveTargetCD>, RefRO<LocalTransform>>().WithEntityAccess())
        {
            float3 targetPosition = moveTarget.ValueRO.targetPosition;
            if (transform.ValueRO.Position.IsArrive(targetPosition) == true)
            {
                ecb.SetComponentEnabled<StateTags.IsArrived>(entity, true);
            }
        }

        BulletHitJob hitJob = new BulletHitJob()
        {
            ecb = ecb.AsParallelWriter(),

            teamLookup = SystemAPI.GetComponentLookup<TeamCD>(true),
            hpLookup = SystemAPI.GetComponentLookup<HPCD>(),
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