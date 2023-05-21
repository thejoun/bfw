using ECS.Component;
using Unity.Entities;
using UnityEngine;

namespace ECS.Authoring
{
    public class ExecuteMain : MonoBehaviour
    {
        private class Baker : Baker<ExecuteMain>
        {
            public override void Bake(ExecuteMain authoring)
            {
                var entity = GetEntity(TransformUsageFlags.None);

                AddComponent<Component.ExecuteMain>(entity);
            }
        }
    }

    
}