using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace HelloCube._9._Bullet
{
    public class EnemyAuthoring : MonoBehaviour
    {
        public GameObject Prefab;
        public float generateInterval = 1.1f;

        class Baker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new EnemyData
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    generateInterval = authoring.generateInterval,
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
    }
}