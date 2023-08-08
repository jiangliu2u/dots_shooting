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
    public partial struct BulletMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MoveSpeed>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;
            new BulletMoveJob
            {
                deltaTime = deltaTime,
            }.ScheduleParallel();
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