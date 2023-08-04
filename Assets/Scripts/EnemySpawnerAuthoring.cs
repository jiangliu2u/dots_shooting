using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube._9._Bullet
{
    public class EnemySpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float generateInterval = 1.1f;

        class Baker : Baker<EnemySpawnerAuthoring>
        {
            public override void Bake(EnemySpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new EnemyData
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    generateInterval = authoring.generateInterval,
                });
                AddComponent(entity, new EnemyTag()
                {
                });
            }
        }
    }

    public struct EnemyData : IComponentData
    {
        public Entity Prefab;
        public double generateInterval;
    }

    public struct EnemyTag : IComponentData
    {
    }

    public readonly partial struct EnemyMoveIAspect : IAspect
    {
        private readonly RefRO<EnemyTag> enemyTag;
        public readonly RefRW<LocalTransform> localtransform;
        public readonly RefRO<MoveSpeed> speed;
        public readonly Entity Self;


        public bool Move(float deltaTime)
        {
            var old = localtransform.ValueRW.Position;
            var v = math.normalize(speed.ValueRO.target - old);
            
            var lll = math.atan2(v[0], v[1]);
            var a = new float3(deltaTime * speed.ValueRO.speed * math.sin(lll),
                deltaTime * speed.ValueRO.speed * math.cos(lll), 0f);

            if (math.distance(localtransform.ValueRW.Position, speed.ValueRO.target) <= 0.4f)
            {
                return true;
            }

            localtransform.ValueRW.Position += a;
            var rotation = quaternion.AxisAngle(math.back(), lll);
            localtransform.ValueRW.Rotation = rotation;
            return false;
        }
    }
}