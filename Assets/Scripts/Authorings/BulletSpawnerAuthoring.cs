using Shooting;
using Shooting.Components;
using Unity.Entities;
using UnityEngine;

namespace Shooting.Authorings
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
}