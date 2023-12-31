using Shooting.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace Shooting.Systems
{
    public partial struct LifeSpanSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var now = SystemAPI.Time.ElapsedTime;
            var ecb = new EntityCommandBuffer(allocator: Allocator.Temp);
            foreach (var (lifeSpan, entity)in SystemAPI.Query<LifeSpanComponent>().WithEntityAccess())
            {
                if ((now - lifeSpan.createTime) > lifeSpan.span)
                {
                    ecb.DestroyEntity(entity);
                }
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}