using Shooting.Aspects;
using Unity.Burst;
using Unity.Entities;

namespace Shooting.Systems
{
    [BurstCompile]
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
    [UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
    public partial struct ApplyDamageSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
        }
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Dependency.Complete();   
            foreach (var player in SystemAPI.Query<PlayerHealthAspect>())
            {
                player.TakeDamage();
            }
        }
    }
}