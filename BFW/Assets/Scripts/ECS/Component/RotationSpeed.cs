using Unity.Entities;

namespace ECS.Component
{
    struct RotationSpeed : IComponentData
    {
        public float RadiansPerSecond;
    }
}