using Unity.Entities;

public struct Tags : IComponentData
{
    public struct Enemy : IComponentData { }
    public struct Tower : IComponentData { }
    public struct Bullet : IComponentData { }
}


public struct StateTags : IComponentData
{
    public struct AttackReady : IComponentData, IEnableableComponent { }
    public struct IsArrived : IComponentData, IEnableableComponent { }
    public struct IsAlive : IComponentData, IEnableableComponent { }
}