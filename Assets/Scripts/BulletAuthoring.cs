using Unity.Entities;
using UnityEngine;

namespace HelloCube._9._Bullet
{
    public class BulletAuthoring : MonoBehaviour
    {
        [SerializeField] private double lifeSpan;

        class BulletBaker : Baker<BulletAuthoring>
        {
            public override void Bake(BulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BulletTag
                {
                });
                AddComponent(entity, new LifeSpanComponent()
                {
                    createTime = 0,
                    span = authoring.lifeSpan
                });
            }
        }
    }
}