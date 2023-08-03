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
            var ecb = new EntityCommandBuffer(Allocator.Temp);
            foreach (var (enemyMoveIAspect, entity) in
                     SystemAPI.Query<EnemyMoveIAspect>().WithEntityAccess())
            {
                var old = enemyMoveIAspect.localtransform.ValueRW.Position;
                var v = math.normalize(enemyMoveIAspect.speed.ValueRO.target - old);

                var lll = math.atan2(v[0], v[1]);
                var a = new float3(deltaTime * enemyMoveIAspect.speed.ValueRO.speed * math.sin(lll),
                    deltaTime * enemyMoveIAspect.speed.ValueRO.speed * math.cos(lll), 0f);

                if (math.distance( enemyMoveIAspect.localtransform.ValueRW.Position, enemyMoveIAspect.speed.ValueRO.target) <= 1.24f)
                {
                    ecb.DestroyEntity(entity);
                    continue;
                }
                enemyMoveIAspect.localtransform.ValueRW.Position += a;
                var rotation = quaternion.AxisAngle(math.back(), lll);
                enemyMoveIAspect.localtransform.ValueRW.Rotation = rotation;
            }

            ecb.Playback(state.EntityManager);
            ecb.Dispose();
        }
    }
}