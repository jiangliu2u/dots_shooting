using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Shooting.Components
{
    public struct MoveSpeed : IComponentData
    {
        public float speed;
        public float3 target;
        public float angle;
    }
}