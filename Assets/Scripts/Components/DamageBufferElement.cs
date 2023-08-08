using Unity.Entities;

namespace Shooting.Components
{
    [InternalBufferCapacity(50)]
    public struct DamageBufferElement : IBufferElementData
    {
        public int Damage;
    }
}