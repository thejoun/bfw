using UnityEngine;

namespace Game.Components
{
    public class PositionComponent : RemoteComponent
    {
        [field: SerializeField] public Vector2Int Position { get; private set; }

        public virtual void SetPosition(Vector2Int position)
        {
            Position = position;
        }
    }
}