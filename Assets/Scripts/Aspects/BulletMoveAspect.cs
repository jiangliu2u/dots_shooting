using Shooting.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Shooting.Aspects
{
    public readonly partial struct BulletMoveAspect : IAspect
    {
        public readonly RefRW<LocalTransform> localtransform;
        public readonly RefRO<MoveSpeed> speed;
        public readonly RefRO<BulletTag> tag;

        public void Move(float deltaTime)
        {
            var a = new float3(
                deltaTime * speed.ValueRO.speed *
                math.sin(speed.ValueRO.angle),
                deltaTime * speed.ValueRO.speed *
                math.cos(speed.ValueRO.angle), 0f);
            localtransform.ValueRW.Position += a;
            var rotation = quaternion.AxisAngle(math.back(), speed.ValueRO.angle);
            localtransform.ValueRW.Rotation = rotation;
        }
    }
}