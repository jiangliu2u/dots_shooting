using Shooting.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

namespace Shooting.Systems
{
    public partial struct BulletCollideSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BulletTag>();
            state.RequireForUpdate<SimulationSingleton>();
            state.RequireForUpdate<EffectSpawner>();
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
            state.Dependency = new JobCheckCollide
            {
                ecb = ecb,
                enemyLookup = SystemAPI.GetComponentLookup<EnemyTag>(),
                bulletLookup = SystemAPI.GetComponentLookup<BulletTag>(),
                // playerLookup = SystemAPI.GetComponentLookup<PlayerTag>(),
            }.Schedule(SystemAPI.GetSingleton<SimulationSingleton>(), state.Dependency);

            state.Dependency.Complete();
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }

    
        [BurstCompile]
        public partial struct JobCheckCollide : ITriggerEventsJob
        {
            public EntityCommandBuffer ecb;
            public ComponentLookup<EnemyTag> enemyLookup;
            public ComponentLookup<BulletTag> bulletLookup;
            public void Execute(TriggerEvent triggerEvent)
            {
                var isEnemyA = IsEnemy(triggerEvent.EntityA);
                var isBulletA = IsBullet(triggerEvent.EntityA);
                
                var isEnemyB = IsEnemy(triggerEvent.EntityB);
                var isBulletB = IsBullet(triggerEvent.EntityB);
                
                if (isBulletA == isBulletB)
                {
                    return;
                }
                
                if (isEnemyA == isEnemyB)
                {
                    return;
                }
                
                var newEntity = ecb.CreateEntity();
                if (isBulletA)
                {
                    ecb.AddComponent(newEntity,
                        new BulletDamageComponent
                        {
                            Bullet = triggerEvent.EntityA,
                            Target = triggerEvent.EntityB
                        });
                }
                else
                {
                    ecb.AddComponent(newEntity,
                        new BulletDamageComponent
                        {
                            Bullet = triggerEvent.EntityB,
                            Target = triggerEvent.EntityA
                        });
                }
            }
            
            private bool IsEnemy(Entity e)
            {
                return enemyLookup.HasComponent(e);
            }

            private bool IsBullet(Entity e)
            {
                return bulletLookup.HasComponent(e);
            }
        }
    }
}