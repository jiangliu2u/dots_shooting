using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube._9._Bullet
{
    public class SpawnerBulletAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float shootInterval = 0.1f;
        public float lifeSpan = 3;
        class Baker : Baker<SpawnerBulletAuthoring>
        {
            public override void Bake(SpawnerBulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new SpawnerBullet
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    shootInterval = authoring.shootInterval,
                    span = authoring.lifeSpan
                });
                AddComponent(entity, new BulletTag
                {
                });
                AddComponent(entity, new LifeSpanComponent()
                {
                    createTime = Time.time,
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
    }
}