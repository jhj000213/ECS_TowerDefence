using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

partial struct TowerCreateSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            JDebugLogger.Log("?");

            var entityManager = state.EntityManager;

            var spawnerEntity = SystemAPI.GetSingletonEntity<TowerPrefabsCD>();
            var towerPrefabsCD = SystemAPI.GetComponentRW<TowerPrefabsCD>(spawnerEntity).ValueRO;


            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();
            EntityCommandBuffer ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

            Entity newEntity1 = ecb.Instantiate(towerPrefabsCD.tower);

            ecb.AddComponent(newEntity1, new TeamCD()
            {
                team = Team.TOWER,
            });

            ecb.AddComponent(newEntity1, new Parent { Value = towerPrefabsCD.towerParent });
            ecb.SetComponent(newEntity1, LocalTransform.FromPosition(0,0,0));

            AddAttackComponent(ref ecb, ref newEntity1);

#if UNITY_EDITOR
            ecb.SetName(towerPrefabsCD.towerParent, "TowerParent");
            ecb.SetName(newEntity1, "Tower");
#endif
        }
    }

    void AddAttackComponent(ref EntityCommandBuffer ecb, ref Entity tower)
    {
        var attackObjPrefabsCD = SystemAPI.GetSingleton<AttackObjectPrefabsCD>();

        ecb.AddComponent(tower, new AttackCD(1.5f, 5f, 0.8f, attackObjPrefabsCD.bullet));
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
