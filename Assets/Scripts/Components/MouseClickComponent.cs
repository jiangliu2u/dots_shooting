using Unity.Entities;
using Unity.Mathematics;

public struct MouseClickComponent : IComponentData
{
    public bool clicked;
    public bool moving;
    public float3 pos;
    public float3 target;
    public float3 hoverTarget;
}