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
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (tbBulletMoveAspect, entity) in
                     SystemAPI.Query<BulletMoveAspect>().WithEntityAccess())
            {
                var old = tbBulletMoveAspect.localtransform.ValueRW.Position;
                var v = math.normalize(tbBulletMoveAspect.speed.ValueRO.target - old);

                var lll = math.atan2(v[0], v[1]);
                var a = new float3(deltaTime * tbBulletMoveAspect.speed.ValueRO.speed * math.sin(lll),
                    deltaTime * tbBulletMoveAspect.speed.ValueRO.speed * math.cos(lll), 0f);

                // if (math.distance( tbBulletMoveAspect.localtransform.ValueRW.Position, tbBulletMoveAspect.speed.ValueRO.target) <= 1.24f)
                // {
                //     ecb.DestroyEntity(entity);
                //     continue;
                // }
                tbBulletMoveAspect.localtransform.ValueRW.Position += a;
                var rotation = quaternion.AxisAngle(math.back(), lll);
                tbBulletMoveAspect.localtransform.ValueRW.Rotation = rotation;
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}