using Unity.Entities;
using UnityEngine;

class BulletAuthoring : MonoBehaviour
{
    
}

class BulletAuthoringBaker : Baker<BulletAuthoring>
{
    public override void Bake(BulletAuthoring authoring)
    {

        var bullet = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(bullet, new MoveCD
        {
            speed = 5f
        });
        AddComponent(bullet, new Tags.Bullet());



        //AddComponent(bullet, new BulletObjectCD
        //{
        //    transform = authoring.transform//,
        //    //animator = authoring.animator_;
        //});
    }
}
