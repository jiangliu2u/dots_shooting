using System;
using Shooting;
using Shooting.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace Shooting.Authorings
{
    public class MoveSpeedAuthoring : MonoBehaviour
    {
        public float movePerSecond = 5f;
        public System.Random seed = new System.Random();

        private void Awake()
        {
        }

        class Baker : Baker<MoveSpeedAuthoring>
        {
            public override void Bake(MoveSpeedAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new MoveSpeed()
                {
                    speed = authoring.movePerSecond,
                });
            }
        }
    }

    

   
}