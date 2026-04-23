using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

partial struct ToTargetMoveTowardSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        foreach (var transform in SystemAPI.Query<LocalTransform>().WithAll<MovePathCD, MoveCD, LocalTransform>())
        {
            CheckTargetPositionJob arrivedCheckJob = new CheckTargetPositionJob();

            MoveTowardJob moveJob = new MoveTowardJob()
            {
                deltaTime = SystemAPI.Time.DeltaTime,
            };


            JobHandle arriveHandle = arrivedCheckJob.ScheduleParallel(state.Dependency);
            JobHandle moveHandle = moveJob.ScheduleParallel(arriveHandle);

            state.Dependency = moveHandle;
        }
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
