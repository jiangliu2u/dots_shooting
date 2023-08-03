using Unity.Entities;

namespace HelloCube._9._Bullet
{
    public struct LifeSpanComponent:IComponentData
    {
        public double span;
        public double createTime;
    }
}