using Unity.Entities;

namespace Shooting.Components
{
    [InternalBufferCapacity(20)]
    public struct DamageBufferElement:IBufferElementData
    {
        public int Value;
    }
}