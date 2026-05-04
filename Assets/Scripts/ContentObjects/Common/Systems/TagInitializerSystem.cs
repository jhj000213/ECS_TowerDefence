using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[UpdateInGroup(typeof(InitializationSystemGroup))] // 로직 시작 전 초기화 단계에서 실행
public partial struct TagInitializerSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        var ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>()
                           .CreateCommandBuffer(state.WorldUnmanaged);

        // 'AliveTag'가 없는(None) 엔티티들만 쿼리해서 가져옵니다.
        // 특정 컴포넌트(예: LocalTransform)가 있는 것들로 한정하는 것이 안전합니다.
        foreach (var (transform, entity) in SystemAPI.Query<RefRO<LocalTransform>>()
                                                   .WithNone<StateTags.IsAlive>()
                                                   .WithEntityAccess())
        {
            ecb.AddComponent<StateTags.IsAlive>(entity);
            // 생성 직후 Enabled 상태를 제어하고 싶다면 여기서 처리
            ecb.SetComponentEnabled<StateTags.IsAlive>(entity, true);
        }
    }
}