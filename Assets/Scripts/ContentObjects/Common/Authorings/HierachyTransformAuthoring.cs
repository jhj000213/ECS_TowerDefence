using Unity.Entities;
using UnityEngine;

class HierachyTransformAuthoring : MonoBehaviour
{
    
}

class HierachyTransformAuthoringBaker : Baker<HierachyTransformAuthoring>
{
    public override void Bake(HierachyTransformAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
    }
}
