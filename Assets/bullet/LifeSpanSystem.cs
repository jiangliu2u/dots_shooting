using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

namespace HelloCube._9._Bullet
{
    public partial struct LifeSpanSystem:ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var now = SystemAPI.Time.ElapsedTime;
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (lifeSpan,entity )in SystemAPI.Query<LifeSpanComponent>().WithEntityAccess())
            {
                if (lifeSpan.createTime - now > lifeSpan.span)
                {
                    ecb.DestroyEntity(entity);
                }
            }
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
    
    
}