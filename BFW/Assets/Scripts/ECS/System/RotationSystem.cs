using ECS.Component;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace ECS.System
{
    public partial struct RotationSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<ExecuteMain>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (transformRef, speedRef) in 
                     SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotationSpeed>>())
            {
                var rotateY = transformRef.ValueRO.RotateY(
                    speedRef.ValueRO.RadiansPerSecond * deltaTime);
                
                transformRef.ValueRW = rotateY;
            }
        }
    }
}