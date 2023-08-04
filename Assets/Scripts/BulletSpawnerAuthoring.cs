using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube._9._Bullet
{
    public class BulletSpawnerAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float shootInterval = 0.1f;
        public float lifeSpan = 3;

        class Baker : Baker<BulletSpawnerAuthoring>
        {
            public override void Bake(BulletSpawnerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new SpawnerBullet
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    shootInterval = authoring.shootInterval,
                    span = authoring.lifeSpan
                });
            }
        }
    }


    public struct SpawnerBullet : IComponentData
    {
        public Entity Prefab;
        public double shootInterval;
        public double span;
    }

    public struct BulletTag : IComponentData
    {
    }

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