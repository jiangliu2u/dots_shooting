using Shooting;
using Shooting.Components;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace Shooting.Authorings
{
    public class PlayerAuthoring : MonoBehaviour
    {
        public float speed = 21.0f;

        class PlayerAuthoringBaker : Baker<PlayerAuthoring>
        {
            public override void Bake(PlayerAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);
                AddComponent(entity, new PlayerMove()
                {
                    speed = authoring.speed
                });
                AddComponent(entity, new PlayerTag() { });
                AddComponent(entity, new HealthComponent()
                {
                    HealthMax = 200,
                    Health = 200
                });
            }
        }
    }
}