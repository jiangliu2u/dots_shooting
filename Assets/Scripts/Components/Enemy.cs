using Unity.Entities;

namespace Shooting.Components
{
    public struct EnemyData : IComponentData
    {
        public Entity Prefab;
        public double generateInterval;
    }

    public struct EnemyTag : IComponentData
    {
    }
}