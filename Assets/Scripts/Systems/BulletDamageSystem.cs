using Shooting.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Shooting.Systems
{
    public partial struct BulletDamageSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<BulletDamageComponent>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            EntityCommandBuffer ecb = new EntityCommandBuffer(Allocator.TempJob);
            var p = SystemAPI.GetSingleton<EffectSpawner>();

            state.Dependency = new BulletDamageJob
            {
                ECB = ecb,
                EM = state.EntityManager,
                enemyLookup = SystemAPI.GetComponentLookup<EnemyTag>(),
                effect = p,
                time = SystemAPI.Time.ElapsedTime
            }.Schedule(state.Dependency);

            state.Dependency.Complete();
            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }

    [BurstCompile]
    public partial struct BulletDamageJob : IJobEntity
    {
        public EntityCommandBuffer ECB;
        public EntityManager EM;
        public ComponentLookup<EnemyTag> enemyLookup;
        public EffectSpawner effect;
        public double time;

        void Execute(RefRW<BulletDamageComponent> bulletDamageComponent, Entity entity)
        {
            var bulletEntity = bulletDamageComponent.ValueRW.Bullet;
            var targetEntity = bulletDamageComponent.ValueRW.Target;

            BulletTag bulletComponent = EM.GetComponentData<BulletTag>(bulletEntity);
            BulletDamage damageComponent = EM.GetComponentData<BulletDamage>(bulletEntity);


            var effectEntity = ECB.Instantiate(effect.Prefab);
            
            if ((enemyLookup.HasComponent(targetEntity)))
            {
                ECB.AddComponent(targetEntity, new DamageEffectComponent() { Damage = damageComponent.Damage });
                ECB.DestroyEntity(bulletEntity);
                ECB.DestroyEntity(targetEntity);
            }

            ECB.DestroyEntity(entity);
        }
    }
}