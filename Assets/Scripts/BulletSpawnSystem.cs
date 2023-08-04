using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;

namespace HelloCube._9._Bullet
{
    public partial struct BulletSpawnSystem : ISystem
    {
        uint updateCounter;
        private EntityQuery bulletQuery;
        private double lastShootTime;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            // This call makes the system not update unless at least one entity in the world exists that has the Spawner component.
            state.RequireForUpdate<SpawnerBullet>();
            lastShootTime = 0;
            bulletQuery = SystemAPI.QueryBuilder().WithAll<MoveSpeed>().Build();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var click = SystemAPI.GetSingleton<MouseClickComponent>();
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            if (click.clicked)
            {
               
                var p = SystemAPI.GetSingleton<SpawnerBullet>();
                var player = SystemAPI.GetSingletonEntity<PlayerTag>();
                var playerTrans = state.EntityManager.GetComponentData<LocalTransform>(player);
                if (SystemAPI.Time.ElapsedTime - lastShootTime > p.shootInterval)
                {
                    lastShootTime = SystemAPI.Time.ElapsedTime;
                    var bullet = state.EntityManager.Instantiate(p.Prefab);
                    var bulletTransform =
                        LocalTransform.FromPositionRotationScale(playerTrans.Position, quaternion.identity, 1f);
                    var v = math.normalize(click.hoverTarget - bulletTransform.Position);
                    var angle = math.atan2(v[0], v[1]);
                    var rotation = quaternion.AxisAngle(math.back(), math.radians(angle));
                    bulletTransform.Rotation = rotation;
                    state.EntityManager.SetComponentData(bullet, bulletTransform);
                    state.EntityManager.AddComponentData(bullet, new MoveSpeed()
                    {
                        direction = click.hoverTarget,
                        speed = 15,
                        target = click.hoverTarget,
                        angle = angle
                    });
                    LifeSpanComponent lifeSpanComponent = state.EntityManager.GetComponentData<LifeSpanComponent>(bullet);
                    lifeSpanComponent.createTime = lastShootTime;
                    state.EntityManager.SetComponentData(bullet, lifeSpanComponent);
                }
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}