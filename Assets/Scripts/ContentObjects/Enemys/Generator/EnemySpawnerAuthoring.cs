using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

class EnemySpawnerAuthoring : MonoBehaviour
{
    public EnemyUnitAuthoring enemyPrefab_;
    public Transform[] enemyMovePathTransforms_ = new Transform[4];
}

class EnemySpawnerAuthoringBaker : Baker<EnemySpawnerAuthoring>
{
    public override void Bake(EnemySpawnerAuthoring authoring)
    {
        var spawner = GetEntity(TransformUsageFlags.None);


        EnemyPrefabsCD enemyPrefabsCD = new EnemyPrefabsCD
        {
            unitPrefab = GetEntity(authoring.enemyPrefab_, TransformUsageFlags.Dynamic),
        };

        Transform[] arrPosition = authoring.enemyMovePathTransforms_;
        EnemyMovePathCD movePathCD = new EnemyMovePathCD();

        for (int i = 0; i < arrPosition.Length; ++i)
        {
            movePathCD.positions.Add(new float3(arrPosition[i].position));
        }


        AddComponent(spawner, enemyPrefabsCD);
        AddComponent(spawner, movePathCD);


    }
}
