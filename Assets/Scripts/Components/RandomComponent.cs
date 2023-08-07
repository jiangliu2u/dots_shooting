using Unity.Entities;
using Unity.Mathematics;
namespace Shooting.Components
{
    public struct RandomComponent : IComponentData
    {
        public Random random;
    }
}