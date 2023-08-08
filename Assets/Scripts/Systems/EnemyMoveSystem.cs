using Shooting;
using Shooting.Aspects;
using Shooting.Components;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Shooting.Systems
{
    public partial struct EnemyMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            // This call makes the system not update unless at least one entity in the world exists that has the Spawner component.
            state.RequireForUpdate<EnemyData>();
            // state.RequireForUpdate<Execute.Prefabs>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
            var playerEntity = SystemAPI.GetSingletonEntity<PlayerTag>();
            var playerTransform = SystemAPI.GetComponent<LocalTransform>(playerEntity);
            new EnemyMoveJob
            {
                PlayerEntity = playerEntity,
                PlayerTransform = playerTransform,
                ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter(),
                deltaTime = deltaTime
            }.ScheduleParallel();
        }
    }

    public partial struct EnemyMoveJob : IJobEntity
    {
        public float deltaTime;
        public EntityCommandBuffer.ParallelWriter ecb;
        public Entity PlayerEntity;
        public LocalTransform PlayerTransform;

        public void Execute(EnemyIAspect iAspect, [EntityIndexInChunk] int sortKey)
        {
            if (iAspect.CheckHitPlayer(PlayerTransform))
            {
                iAspect.Damage(ecb, sortKey, PlayerEntity);
                ecb.DestroyEntity(sortKey, iAspect.Self);
                return;
            }
            
            if (iAspect.Move(deltaTime))
            {
                ecb.DestroyEntity(sortKey, iAspect.Self);
            }
        }
    }
}