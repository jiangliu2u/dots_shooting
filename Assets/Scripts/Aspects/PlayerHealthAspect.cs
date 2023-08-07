using Shooting.Components;
using Unity.Entities;
using Unity.Transforms;

namespace Shooting.Aspects
{
    public readonly partial struct PlayerHealthAspect : IAspect
    {
        private readonly RefRO<PlayerTag> _playerTag;
        public readonly RefRW<HealthComponent> healthComponent;
    }
}