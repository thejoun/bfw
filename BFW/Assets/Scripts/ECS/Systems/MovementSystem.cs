using System.Collections.Generic;
using System.Linq;
using Const;
using ECS.Components;
using Enums;
using Extensions;
using Interfaces;
using Zenject;

namespace ECS.Systems
{
    public class MovementSystem : ExecutableSystem<IEnumerable<HexDirection>>
    {
        [Inject(Id = ID.EntityRegistry)] private IRegistry<IEntity> entities;

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

        protected override void ExecuteRemote(int entityId, IEnumerable<HexDirection> stepDirections)
        {
            var webContract = web.GetContract(contract);
            var function = webContract.GetFunction("executeTyped");
            var stepBytes = stepDirections.Select(step => (byte)(int)step).ToArray();
            
            function.ExecuteAsync(account, gasLimit, entityId, stepBytes)
                .WithCallback(OnReceiptReceived);
        }
    }
}