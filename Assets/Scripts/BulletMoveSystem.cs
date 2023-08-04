using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube._9._Bullet
{
    public partial struct BulletMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            // This call makes the system not update unless at least one entity in the world exists that has the Spawner component.
            state.RequireForUpdate<MoveSpeed>();
            // state.RequireForUpdate<Execute.Prefabs>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            new BulletMoveJob
            {
                deltaTime = deltaTime,
            }.Schedule();
        }
    }

    public partial struct BulletMoveJob : IJobEntity
    {
        public float deltaTime;

        public void Execute(BulletMoveAspect bulletMoveAspect)
        {
            bulletMoveAspect.Move(deltaTime);
        }
    }
}