using Unity.Entities;
using UnityEngine;

class EnemyUnitAuthoring : MonoBehaviour
{
    
}

class EnemyUnitAuthoringBaker : Baker<EnemyUnitAuthoring>
{
    public override void Bake(EnemyUnitAuthoring authoring)
    {
        var enemyEntity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(enemyEntity, new EnemyObjectCD 
        {
            transform = authoring.transform//,
            //animator = authoring.animator_;
        });
        
    }
}
