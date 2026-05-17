using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

partial struct MoveSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
        EntityCommandBuffer ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);


        CheckTargetPositionJob arrivedCheckJob = new CheckTargetPositionJob();

        MoveToPathJob moveToPathJob = new MoveToPathJob()
        {
            deltaTime = SystemAPI.Time.DeltaTime,
        };

        MoveToTargetJob moveToTargetJob = new MoveToTargetJob()
        {
            beginEcb = ecb.AsParallelWriter(),
            deltaTime = SystemAPI.Time.DeltaTime,
        };


        //foreach(var (transform, entity) in SystemAPI.Query<RefRO<Tags.Bullet>>().WithEntityAccess())
        //{
        //    NativeArray<ComponentType> componentTypes = state.EntityManager.GetComponentTypes(entity);

        //    JDebugLogger.Log($"--- Entity [{entity.Index}]가 보유한 컴포넌트 목록 ---");

        //    // 2. 루프를 돌며 타입 이름 출력
        //    for (int i = 0; i < componentTypes.Length; i++)
        //    {
        //        JDebugLogger.Log($"- Component {i}: {componentTypes[i].GetManagedType().Name}");
        //    }
        //    componentTypes.Dispose();
        //}


        JobHandle arriveHandle = arrivedCheckJob.ScheduleParallel(state.Dependency);
        JobHandle moveToPathHandle = moveToPathJob.ScheduleParallel(arriveHandle);
        JobHandle moveToTargetHandle = moveToTargetJob.ScheduleParallel(moveToPathHandle);

        state.Dependency = moveToTargetHandle;
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
