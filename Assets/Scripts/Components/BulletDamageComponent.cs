using Unity.Entities;

namespace Shooting.Components
{
    public struct BulletDamageComponent : IComponentData
    {
        public Entity Bullet;
        public Entity Target;
    }
}