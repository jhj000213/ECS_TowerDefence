using Unity.Burst;
using Unity.Entities;
using UnityEngine;

partial struct CreatorAdminSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var entityManager = state.EntityManager;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach( var hpCD in SystemAPI.Query<RefRW<HPCD>>().WithAll<Tags.Enemy>())
            {
                hpCD.ValueRW.SetDamage(1.0f);
            }
        }
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
