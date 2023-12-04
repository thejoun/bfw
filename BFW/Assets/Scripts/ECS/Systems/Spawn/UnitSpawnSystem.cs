using UnityEngine;

namespace ECS.Systems
{
    public class UnitSpawnSystem : EntityCreationSystem<(int archetypeId, Vector2Int position)>
    {
        protected override object[] GetRemoteArguments((int archetypeId, Vector2Int position) value)
        {
            return new object[] { value.archetypeId, value.position.x, value.position.y };
        }
    }
}