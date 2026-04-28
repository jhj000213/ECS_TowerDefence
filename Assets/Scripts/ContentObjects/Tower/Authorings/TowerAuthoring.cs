using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

class TowerAuthoring : MonoBehaviour
{
    public Transform bulletStartPosition_;

}

class TowerAuthoringBaker : Baker<TowerAuthoring>
{
    public override void Bake(TowerAuthoring authoring)
    {
        var towerEntity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(towerEntity, new TowerCD
        {
            bulletStartPosition = new float3(authoring.bulletStartPosition_)//,
            //animator = authoring.animator_;
        });
        AddComponent(towerEntity, new MoveCD
        {
            speed = 3f
        });
        AddComponent(towerEntity, new Tags.Tower());
    }
}
