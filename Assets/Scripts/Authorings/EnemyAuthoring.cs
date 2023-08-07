using Shooting;
using Shooting.Components;
using Unity.Entities;
using UnityEngine;

namespace Shooting.Authorings
{
    public class EnemyAuthoring : MonoBehaviour
    {
        [SerializeField] private float span = 4;
        class EnemyBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new EnemyTag()
                {
                });
            }
        }
    }
}