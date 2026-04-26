using Unity.Entities;
using UnityEngine;

public struct EnemyObjectCD : IComponentData
{
    public UnityObjectRef<Transform> transform;
   // public UnityObjectRef<Animator> animator;
}
