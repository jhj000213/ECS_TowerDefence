using Unity.Entities;
using UnityEngine;

class TowerSpawnerAuthoring : MonoBehaviour
{
    public TowerAuthoring towerPrefab_;
    public Transform towerParentTransform_;
}

class TowerSpawnerAuthoringBaker : Baker<TowerSpawnerAuthoring>
{
    public override void Bake(TowerSpawnerAuthoring authoring)
    {
        var spawner = GetEntity(TransformUsageFlags.None);


        TowerPrefabsCD towerPrefabs = new TowerPrefabsCD
        {
            tower = GetEntity(authoring.towerPrefab_, TransformUsageFlags.Dynamic),
            towerParent = GetEntity(authoring.towerParentTransform_, TransformUsageFlags.Dynamic),
        };

        AddComponent(spawner, towerPrefabs);
    }
}
