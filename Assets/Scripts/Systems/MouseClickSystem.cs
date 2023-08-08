using Shooting;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Random = UnityEngine.Random;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial class MouseClickSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<MouseClickComponent>();
    }


    protected override void OnUpdate()
    {
        var entity = SystemAPI.GetSingletonEntity<MouseClickComponent>();
        var mouseClick = EntityManager.GetComponentData<MouseClickComponent>(entity);
        if (Input.GetMouseButtonDown(0))
        {
            mouseClick.pos = new float3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }

        Ray unityRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            mouseClick.clicked = true;
            mouseClick.target = new float3(unityRay.origin.x, unityRay.origin.y, 0);
        }

        mouseClick.hoverTarget = new float3(unityRay.origin.x, unityRay.origin.y, 0);
        if (Input.GetMouseButtonUp(0))
        {
            mouseClick.clicked = false;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            mouseClick.moving = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            mouseClick.moving = false;
        }
        mouseClick.moving = true;

        EntityManager.SetComponentData(entity, mouseClick);
    }
}