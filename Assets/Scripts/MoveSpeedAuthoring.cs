using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

namespace HelloCube._9._Bullet
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
                var x = Random.Range(-0.5f, 0.21f);
                var y = Random.Range(-0.5f, 0.1f);
                Ray unityRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                float3 target = new float3(unityRay.origin.x, unityRay.origin.y, 0);
                AddComponent(entity, new MoveSpeed()
                {
                    speed = authoring.movePerSecond,
                    direction = Input.mousePosition,
                    target = target
                });
            }
        }
    }

    public struct MoveSpeed : IComponentData
    {
        public float speed;
        public Vector3 direction;
        public float3 target;
        public float angle;
    }

    public struct PlayerMove : IComponentData
    {
        public float speed;
        public Entity entity;
    }
}