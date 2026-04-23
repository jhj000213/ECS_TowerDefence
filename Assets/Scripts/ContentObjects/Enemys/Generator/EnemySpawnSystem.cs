using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial struct EnemySpawnSystem : ISystem
{
    float elapsedTime;
    const float spawnDelayTime = 1.0f;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        elapsedTime = 0;
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //elapsedTime += SystemAPI.Time.DeltaTime;

        if(Input.GetKeyDown(KeyCode.Space) || elapsedTime > spawnDelayTime)
        {

            var entityManager = state.EntityManager;

            var spawnerEntity = SystemAPI.GetSingletonEntity<EnemyPrefabsSCD>();
            EnemyPrefabsSCD enemyPrefabsCD = entityManager.GetComponentData<EnemyPrefabsSCD>(spawnerEntity);
            MovePathCD movePathCD = entityManager.GetComponentData<MovePathCD>(spawnerEntity);

            Entity newEntity = entityManager.Instantiate(enemyPrefabsCD.unitPrefab);
            entityManager.AddComponentData(newEntity, new MoveCD() { speed = 0.5f, });
            entityManager.AddComponentData(newEntity, new MovePathCD(movePathCD.PositionList));
            entityManager.SetComponentData(newEntity, LocalTransform.FromPosition(movePathCD[0]));

            elapsedTime -= spawnDelayTime;

        }


    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }
}
