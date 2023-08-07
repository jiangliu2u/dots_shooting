using Shooting.Components;
using Unity.Entities;
using UnityEngine;

namespace Shooting.Authorings
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

    
}