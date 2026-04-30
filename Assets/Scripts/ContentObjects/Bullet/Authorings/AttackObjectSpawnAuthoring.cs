using Unity.Entities;
using UnityEngine;

class AttackObjectSpawnAuthoring : MonoBehaviour
{
    public Transform objParentTransform_;

    public BulletAuthoring bulletPrefab_;
}

class AttackObjectSpawnAuthoringBaker : Baker<AttackObjectSpawnAuthoring>
{
    public override void Bake(AttackObjectSpawnAuthoring authoring)
    {
        var spawner = GetEntity(TransformUsageFlags.None);


        AttackObjectPrefabsCD bulletPrefabs = new AttackObjectPrefabsCD
        {
            attackObjectParent = GetEntity(authoring.objParentTransform_, TransformUsageFlags.Dynamic),

            bullet = GetEntity(authoring.bulletPrefab_, TransformUsageFlags.Dynamic),
        };

        AddComponent(spawner, bulletPrefabs);
    }
}