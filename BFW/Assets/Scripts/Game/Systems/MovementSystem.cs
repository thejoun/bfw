using System.Collections.Generic;
using System.Linq;
using Enums;
using Extensions;
using Game.Components;
using Interfaces;
using UnityEngine;
using Zenject;

namespace Game.Systems
{
    public class MovementSystem : ExecutableSystem<List<HexDirection>>
    {
        [Inject] private IContract contract;
        
        [SerializeField] private PositionComponent positionComponent;

        private Vector2Int Position => positionComponent.Value;

        protected override void Init()
        {
            base.Init();

            positionComponent = GetComponent<PositionComponent>();
        }

        public override void Execute(List<HexDirection> steps)
        {
            MoveRemote(steps);
            MoveLocal(steps);
        }

        protected virtual void MoveLocal(IEnumerable<HexDirection> steps)
        {
            foreach (var step in steps)
            {
                MoveLocal(step);
            }
        }

        protected virtual void MoveLocal(HexDirection step)
        {
            var vector = step.VectorFrom(Position.x);

            var newPosition = Position + vector;
            
            positionComponent.SetValue(newPosition);
        }

        protected virtual void MoveRemote(IEnumerable<HexDirection> stepDirections)
        {
            var webContract = web.GetContract(contract);
            var function = webContract.GetFunction("executeTyped");
            var stepBytes = stepDirections.Select(step => (byte)(int)step).ToArray();
            
            function.ExecuteAsync(account, gasLimit, Entity.Id, stepBytes)
                .WithCallback(OnReceiptReceived);
        }
    }
}