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

        MoveToPathJob moveToPathJob = new MoveToPathJob()
        {
            deltaTime = SystemAPI.Time.DeltaTime,
        };

        MoveToTargetJob moveToTargetJob = new MoveToTargetJob()
        {
            deltaTime = SystemAPI.Time.DeltaTime,
        };


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
