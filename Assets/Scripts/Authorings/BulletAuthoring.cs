using Shooting;
using Shooting.Components;
using Unity.Entities;
using UnityEngine;

namespace Shooting.Authorings
{
    public class BulletAuthoring : MonoBehaviour
    {
        [SerializeField] private double lifeSpan;
        [SerializeField] private float damage;

        class BulletBaker : Baker<BulletAuthoring>
        {
            public override void Bake(BulletAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new BulletTag
                {
                    isEnemy = true,
                    isPlayer = false
                });
                AddComponent(entity, new LifeSpanComponent()
                {
                    createTime = 0,
                    span = authoring.lifeSpan
                });
                AddComponent(entity, new BulletDamage()
                {
                   Damage = 222,
                });
            }
        }

        public void OnDestroy()
        {
            Debug.Log("bullet on destroy!");
        }
    }
}