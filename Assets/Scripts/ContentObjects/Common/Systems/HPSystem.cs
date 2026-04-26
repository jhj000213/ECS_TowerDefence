using Unity.Burst;
using Unity.Entities;

partial struct HPSystem : ISystem
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

        foreach (var (hpCD, entity) in SystemAPI.Query<RefRO<HPCD>>().WithEntityAccess())
        {
            if(hpCD.ValueRO.IsDead() == true)
            {
                JDebugLogger.Log("Dead");
                ecb.DestroyEntity(entity);
            }
        }



    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
