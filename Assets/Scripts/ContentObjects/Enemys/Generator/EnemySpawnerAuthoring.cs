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


        EnemyPrefabsSCD enemyPrefabsCD = new EnemyPrefabsSCD
        {
            unitPrefab = GetEntity(authoring.enemyPrefab_, TransformUsageFlags.Dynamic),
        };

        Transform[] arrPosition = authoring.enemyMovePathTransforms_;
        FixedList64Bytes<float3> position = new FixedList64Bytes<float3>();

        for (int i = 0; i < arrPosition.Length; ++i)
        {
            position.Add(new float3(arrPosition[i].position));
        }

        MovePathCD movePathCD = new MovePathCD(position);


        AddComponent(spawner, enemyPrefabsCD);
        AddComponent(spawner, movePathCD);


    }
}
