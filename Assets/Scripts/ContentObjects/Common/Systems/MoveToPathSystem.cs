using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;

partial struct MoveToPathSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        CheckTargetPositionJob arrivedCheckJob = new CheckTargetPositionJob();

        MoveToPathJob moveJob = new MoveToPathJob()
        {
            deltaTime = SystemAPI.Time.DeltaTime,
        };


        JobHandle arriveHandle = arrivedCheckJob.ScheduleParallel(state.Dependency);
        JobHandle moveHandle = moveJob.ScheduleParallel(arriveHandle);

        state.Dependency = moveHandle;
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
