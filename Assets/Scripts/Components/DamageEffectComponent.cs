using Unity.Entities;

namespace Shooting.Components
{
    public struct DamageEffectComponent :IComponentData
    {
        public int Damage;
    }
}