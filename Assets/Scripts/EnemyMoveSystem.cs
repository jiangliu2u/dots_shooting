using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube._9._Bullet
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
            var ecb = new EntityCommandBuffer(Allocator.TempJob);
            // new EnemyMoveJob
            // {
            //     ecb = ecb,
            //     deltaTime = deltaTime
            // }.Schedule();
            // state.Dependency.Complete();

            foreach (var (moveIAspect, entity)in SystemAPI.Query<EnemyMoveIAspect>().WithEntityAccess())
            {
                if (moveIAspect.Move(deltaTime))
                {
                    ecb.DestroyEntity(entity);
                }
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }

    public partial struct EnemyMoveJob : IJobEntity
    {
        public float deltaTime;
        public EntityCommandBuffer ecb;

        public void Execute(EnemyMoveIAspect moveIAspect, Entity entity)
        {
            if (moveIAspect.Move(deltaTime))
            {
                ecb.DestroyEntity(moveIAspect.Self);
            }
        }
    }
}