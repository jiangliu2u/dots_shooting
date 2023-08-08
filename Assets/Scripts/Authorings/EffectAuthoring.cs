using Shooting.Components;
using Unity.Entities;
using UnityEngine;

namespace Shooting.Authorings
{
    public class EffectAuthoring : MonoBehaviour
    {
        [SerializeField] private GameObject _effectGameObject;
        [SerializeField] private float duration;

        class EffectBaker : Baker<EffectAuthoring>
        {
            public override void Bake(EffectAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new EffectSpawner()
                {
                    Prefab = GetEntity(authoring._effectGameObject, TransformUsageFlags.Dynamic),
                    Duration = authoring.duration
                });
            }
        }
    }
}