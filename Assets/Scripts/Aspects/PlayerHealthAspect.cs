using Shooting.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Shooting.Aspects
{
    public readonly partial struct PlayerHealthAspect : IAspect
    {
        private readonly RefRO<PlayerTag> _playerTag;
        public readonly RefRW<HealthComponent> healthComponent;
        private readonly DynamicBuffer<DamageBufferElement> _damageBuffer;
        
        public void TakeDamage()
        {
            foreach (var damageBufferElement in _damageBuffer)
            {
                healthComponent.ValueRW.Health -= damageBufferElement.Damage;
            }
            _damageBuffer.Clear();
        }
    }
}