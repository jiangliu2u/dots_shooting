using Unity.Entities;

namespace Shooting.Components
{
    public struct PlayerMove : IComponentData
    {
        public float speed;
        public Entity entity;
    }
    public struct PlayerTag : IComponentData
    {
        
    }
}