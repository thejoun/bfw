using System.Collections.Generic;
using System.Linq;
using Const;
using ECS.Components;
using Enums;
using Interfaces;
using Zenject;

namespace ECS.Systems
{
    public class MovementSystem : ExecutableEntitySystem<IEnumerable<HexDirection>>
    {
        [Inject(Id = ID.EntityRegistry)] private IRegistry<IEntity> entities;

        protected override object[] GetRemoteArguments(IEnumerable<HexDirection> value)
        {
            var stepBytes = value.Select(step => (byte)(int)step).ToArray();

            return new object[] { stepBytes };
        }

        protected override void ExecuteLocal(int entityId, IEnumerable<HexDirection> steps)
        {
            if (entities.Values.TryGetFirstWithKey(entityId, out var entity))
            {
                if (entity.TryGetComponent<PositionComponent>(out var positionComponent))
                {
                    var position = positionComponent.Value;
                    
                    foreach (var step in steps)
                    {
                        var vector = step.VectorFrom(position.x);
                        var newPosition = position + vector;
            
                        positionComponent.SetValue(newPosition);
                    }
                }
            }
        }
    }
}