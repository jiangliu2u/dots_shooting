using Shooting.Components;
using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

namespace Shooting.Authorings
{
    public class RandomAuthoring : MonoBehaviour
    {
        class RandomBaker : Baker<RandomAuthoring>
        {
            public override void Bake(RandomAuthoring authoring)
            {
                Entity entiti = GetEntity(TransformUsageFlags.None);
                AddComponent(entiti, new RandomComponent
                {
                    random = Random.CreateFromIndex(1234)
                });
            }
        }
    }
}