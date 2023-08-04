using Unity.Entities;
using UnityEngine;

namespace HelloCube._9._Bullet
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
                // AddComponent(entity, new LifeSpanComponent()
                // {
                //     span = authoring.span
                // });
            }
        }
    }
}