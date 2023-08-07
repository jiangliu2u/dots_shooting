using Unity.Entities;

namespace Shooting.Components
{
    public struct LifeSpanComponent:IComponentData
    {
        public double span;
        public double createTime;
        public Entity entity;
    }
}