using System.Collections.Generic;
using System.Linq;
using ECS.Components;
using Enums;
using Extensions;
using Structs;
using UnityEngine;

namespace ECS.Systems
{
    public class MovementSystem : ExecutableSystem<IEnumerable<HexDirection>>
    {
        [SerializeField] private PositionComponent positionComponent;

        private Vector2Int Position => positionComponent.Value;

        protected override void Init()
        {
            base.Init();

            positionComponent = GetComponent<PositionComponent>();
        }

        protected override void ExecuteLocal(IEnumerable<HexDirection> steps)
        {
            foreach (var step in steps)
            {
                var vector = step.VectorFrom(Position.x);
                var newPosition = Position + vector;
            
                positionComponent.SetValue(newPosition);
            }
        }

        protected override void ExecuteRemote(IEnumerable<HexDirection> stepDirections)
        {
            var webContract = web.GetContract(contract);
            var function = webContract.GetFunction("executeTyped");
            var stepBytes = stepDirections.Select(step => (byte)(int)step).ToArray();
            
            function.ExecuteAsync(account, gasLimit, Entity.Id, stepBytes)
                .WithCallback(OnReceiptReceived);
        }
    }
}