using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECS.Authoring
{
    [AddComponentMenu(nameof(RotationSpeed))]
    public class RotationSpeed : MonoBehaviour
    {
        public float DegreesPerSecond = 360.0f;

        public class Baker : Baker<RotationSpeed>
        {
            public override void Bake(RotationSpeed authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                
                var data = new Component.RotationSpeed
                {
                    RadiansPerSecond = math.radians(authoring.DegreesPerSecond)
                };
                
                AddComponent(entity, data);
            }
        }
    }

}