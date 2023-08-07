using Unity.Entities;

namespace Shooting.Components
{
    public struct SpawnerBullet : IComponentData
    {
        public Entity Prefab;
        public double shootInterval;
        public double span;
    }

    public struct BulletTag : IComponentData
    {
        public bool isEnemy;
        public bool isPlayer;
    } 
    public struct BulletDamage : IComponentData
    {
        public int Damage;
    }
}