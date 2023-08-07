using Shooting;
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
    [UpdateBefore(typeof(TransformSystemGroup))]
    public partial struct PlayerMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<MouseClickComponent>();
            state.RequireForUpdate<PlayerMove>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var click = SystemAPI.GetSingleton<MouseClickComponent>();
            foreach (var moveToPointAspect in
                     SystemAPI.Query<MoveToPointAspect>())
            {
                var angle = moveToPointAspect.Rotate(click);
                moveToPointAspect.Move(click, angle, SystemAPI.Time.DeltaTime);
            }
        }
    }


    public readonly partial struct MoveToPointAspect : IAspect
    {
        public readonly RefRW<LocalTransform> transform;
        public readonly RefRO<PlayerMove> move;


        public float Rotate(MouseClickComponent click)
        {
            var old = transform.ValueRW.Position;
            var v = math.normalize(click.hoverTarget - old);
            var lll = math.atan2(v[0], v[1]);
            var rotation = quaternion.AxisAngle(math.back(), lll);
            if (math.distance(click.hoverTarget, old) > 0.25f)//太近就不用旋转了
            {
                transform.ValueRW.Rotation = math.slerp(transform.ValueRO.Rotation, rotation, 0.2f);
            }
            return lll;
        }

        public void Move(MouseClickComponent click, float angle, float deltaTime)
        {
            if (click.moving)
            {
                var a = new float3(deltaTime * move.ValueRO.speed * math.sin(angle),
                    deltaTime * move.ValueRO.speed * math.cos(angle), 0f);
                var old = transform.ValueRW.Position;
                if (math.distance(click.hoverTarget, old) > 0.25f) //太近就不用动了
                {
                    transform.ValueRW.Position += a;
                }
            }
        }
    }
    // [BurstCompile]
    // public partial struct PlayerMoveJob : IJobEntity
    // {
    //     public float DeltaTime;
    //     
    //     [BurstCompile]
    //     private void Execute(ref LocalTransform transform, in PlayerMoveInput moveInput, PlayerMoveSpeed moveSpeed)
    //     {
    //         
    //         transform.Position.xy += moveSpeed.Value * DeltaTime;
    //        
    //         // if (math.lengthsq(moveInput.Value) > float.Epsilon)
    //         // {
    //         //     var forward = new float3(moveInput.Value.x, 0,moveInput.Value.y);
    //         //     transform.Rotation = quaternion.LookRotation(forward, math.back());
    //         // }
    //     }
    // }
}