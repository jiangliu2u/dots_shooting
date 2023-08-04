using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class MouseClickAuthoring : MonoBehaviour
{
    class MouseClickBaker : Baker<MouseClickAuthoring>
    {
        public override void Bake(MouseClickAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.None);
            AddComponent(entity, new MouseClickComponent
            {
                clicked = false,
                pos = new float3(1, 1, 1),
                target = new float3(1, 1, 1),
                hoverTarget = new float3(1, 1, 1)
            });
        }
    }
}

public struct MouseClickComponent : IComponentData
{
    public bool clicked;
    public bool moving;
    public float3 pos;
    public float3 target;
    public float3 hoverTarget;
}