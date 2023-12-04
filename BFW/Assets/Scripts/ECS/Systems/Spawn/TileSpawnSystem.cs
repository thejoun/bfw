using UnityEngine;

namespace ECS.Systems
{
    public class TileSpawnSystem : EntityCreationSystem<(int terrainId, Vector2Int position)>
    {
        protected override object[] GetRemoteArguments((int terrainId, Vector2Int position) value)
        {
            return new object[] { value.terrainId, value.position.x, value.position.y };
        }
    }
}