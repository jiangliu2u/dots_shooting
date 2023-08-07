using Shooting.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Shooting.Aspects
{
    public readonly partial struct EnemyMoveIAspect : IAspect
    {
        private readonly RefRO<EnemyTag> _enemyTag;
        public readonly RefRW<LocalTransform> Localtransform;
        public readonly RefRO<MoveSpeed> Speed;
        public readonly Entity Self;


        public bool Move(float deltaTime)
        {
            var old = Localtransform.ValueRW.Position;
            var v = math.normalize(Speed.ValueRO.target - old);
            
            var lll = math.atan2(v[0], v[1]);
            var a = new float3(deltaTime * Speed.ValueRO.speed * math.sin(lll),
                deltaTime * Speed.ValueRO.speed * math.cos(lll), 0f);

            if (math.distance(Localtransform.ValueRW.Position, Speed.ValueRO.target) <= 0.4f)
            {
                return true;
            }

            Localtransform.ValueRW.Position += a;
            var rotation = quaternion.AxisAngle(math.back(), lll);
            Localtransform.ValueRW.Rotation = rotation;
            return false;
        }
    }
}