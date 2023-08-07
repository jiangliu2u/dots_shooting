using Unity.Entities;

namespace Shooting.Components
{
    public struct HealthComponent : IComponentData
    {
        public int Health;
        public int HealthMax;
    }
}