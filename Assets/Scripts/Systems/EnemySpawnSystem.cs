using Shooting;
using Shooting.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Shooting.Systems
{
    [UpdateAfter(typeof(PlayerMoveSystem))]
    public partial struct EnemySpawnSystem : ISystem
    {
        private double lastShootTime;

        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            lastShootTime = 0;
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            // var player = SystemAPI.GetSingleton<PlayerMove>();
            //
            // var pEntity = player.entity;
            // if (pEntity == Entity.Null)
            // {
            //     return;
            // }
            //
            // var playerTransform = state.EntityManager.GetComponentData<LocalTransform>(pEntity);
            var p = SystemAPI.GetSingleton<EnemyData>();

            if (SystemAPI.Time.ElapsedTime - lastShootTime > p.generateInterval)
            {
                var ecb = new EntityCommandBuffer(Allocator.TempJob);
                var player = SystemAPI.GetSingletonEntity<PlayerTag>();
                var ran = SystemAPI.GetSingletonRW<RandomComponent>();
                var playerTrans = state.EntityManager.GetComponentData<LocalTransform>(player);
                lastShootTime = SystemAPI.Time.ElapsedTime;
                var enemy = state.EntityManager.Instantiate(p.Prefab);
                var bulletTransform =
                    LocalTransform.FromPositionRotationScale(new float3(ran.ValueRW.random.NextFloat2(-5, 5), 0),
                        quaternion.identity, 1f);
                var v = math.normalize(new float3(0) - bulletTransform.Position);
                var angle = math.atan2(v[0], v[1]);
                var rotation = quaternion.AxisAngle(math.back(), math.radians(angle));
                bulletTransform.Rotation = rotation;
                state.EntityManager.SetComponentData(enemy, bulletTransform);
                state.EntityManager.AddComponentData(enemy, new MoveSpeed()
                {
                    speed = ran.ValueRW.random.NextFloat(1.5f,5.4f),
                    target = playerTrans.Position
                });
                // LifeSpanComponent lifeSpanComponent = state.EntityManager.GetComponentData<LifeSpanComponent>(enemy);
                // lifeSpanComponent.createTime = SystemAPI.Time.ElapsedTime;
                // state.EntityManager.SetComponentData(enemy, lifeSpanComponent);
                ecb.Playback(state.EntityManager);
                ecb.Dispose();
            }
        }
    }
}