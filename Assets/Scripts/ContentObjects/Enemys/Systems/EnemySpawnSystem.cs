using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial struct EnemySpawnSystem : ISystem
{
    float elapsedTime;
    const float spawnDelayTime = 1.0f;
    int spawnCount;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        var entityManager = state.EntityManager;

        elapsedTime = 0;
        spawnCount = 0;


    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        elapsedTime += SystemAPI.Time.DeltaTime;

        if(elapsedTime > spawnDelayTime)
        {
            var entityManager = state.EntityManager;

            var spawnerEntity = SystemAPI.GetSingletonEntity<EnemyPrefabsSCD>();
            var enemyPrefabsCD = SystemAPI.GetComponentRW<EnemyPrefabsSCD>(spawnerEntity).ValueRO;
            var movePathCD = SystemAPI.GetComponentRW<MovePathCD>(spawnerEntity).ValueRO;

            //Entity newEntity = entityManager.Instantiate(enemyPrefabsCD.unitPrefab);
            //entityManager.AddComponentData(newEntity, new MovePathCD(movePathCD.PositionList));
            //entityManager.SetComponentData(newEntity, LocalTransform.FromPosition(movePathCD[0]));

            elapsedTime -= spawnDelayTime;





            var ecbSingleton = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>();

            // 2. 이번 프레임에 사용할 '메모지(CommandBuffer)'를 하나 생성합니다.
            // state.WorldUnmanaged를 넘겨서 어떤 월드에서 실행할지 알려줍니다.
            EntityCommandBuffer ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);
            
            Entity newEntity1 = ecb.Instantiate(enemyPrefabsCD.unit);
            ecb.AddComponent(newEntity1, new MovePathCD(movePathCD.PositionList));

            ecb.AddComponent(newEntity1, new Parent { Value = enemyPrefabsCD.unitParent });
            ecb.SetComponent(newEntity1, LocalTransform.FromPosition(movePathCD[0]));


#if UNITY_EDITOR
            ecb.SetName(enemyPrefabsCD.unitParent, "EnemyParent");
            ecb.SetName(newEntity1, "Enemy" + spawnCount.ToString());
            spawnCount += 1;
#endif
        }


    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
