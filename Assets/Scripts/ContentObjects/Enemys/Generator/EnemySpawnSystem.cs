using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial struct EnemySpawnSystem : ISystem
{
    float elapsedTime;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        elapsedTime = 0;
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        elapsedTime += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            var entityManager = state.EntityManager;

            EnemyPrefabsCD enemyPrefabsCD = SystemAPI.GetSingleton<EnemyPrefabsCD>();
            EnemyMovePathCD movePathCD = SystemAPI.GetSingleton<EnemyMovePathCD>();

            Entity newEntity = entityManager.Instantiate(enemyPrefabsCD.unitPrefab);
            // 4. 생성된 엔티티의 초기 위치 설정 (예: 스포너의 위치)
            state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(movePathCD.positions[0]));
        }


    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
