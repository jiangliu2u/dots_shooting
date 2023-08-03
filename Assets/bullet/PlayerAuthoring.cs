using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace HelloCube._9._Bullet
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
                AddComponent(entity, new PlayerTag(){});
            }
        }
    }

    public struct PlayerTag : IComponentData
    {
        
    }
}