using Unity.Entities;

namespace Shooting.Components
{
    public struct EffectSpawner :IComponentData
    {
        public Entity Prefab;
        public double Duration;
    }
}