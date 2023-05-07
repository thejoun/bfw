using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Context;
using Dtos;
using Enums;
using Extensions;
using Interfaces;
using Nethereum.ABI.FunctionEncoding;
using Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class MovementTest : MonoBehaviour
    {
        [SerializeReference] private IContract positionComponent;
        [SerializeReference] private IContract movementSystem;
        [SerializeReference] private IContract movementPointsComponent;

        [ShowInInspector] private List<HexDirection> steps;

        private IAccount Account => EditorContext.Instance.Account;
        private EcsWeb Web => EditorContext.Instance.Web;
        
        private int Gas => 10000000;
        
        [Button]
        public async void Move(int entity)
        {
            await MoveUnit(entity, steps.ToArray());
        }

        public async Task MoveUnit(int entity, params HexDirection[] stepDirections)
        {
            var contract = Web.GetContract(movementSystem);
            var function = contract.GetFunction("executeTyped");
            var stepBytes = stepDirections.Select(step => (byte)(int)step).ToArray();
            var receipt = await function.ExecuteAsync(Account, Gas, entity, stepBytes);

            Debug.Log($"Moved. Status {receipt.Status}. Gas used {receipt.GasUsed}.");
        }
        
        [Button]
        public async Task CheckMovementPoints(int entity)
        {
            var contract = Web.GetContract(movementPointsComponent);
            var function = contract.GetFunction("getValue");
            
            try
            {
                var result = await function.CallAsync<int>((BigInteger)entity);
                
                Debug.Log($"Entity {entity} has {result} movement points left.");
            }
            catch (SmartContractRevertException revert)
            {
                Debug.Log($"Revert happened: {revert.RevertMessage}");
            }
        }

        [Button]
        public async Task GetPosition(int entity)
        {
            var contract = Web.GetContract(positionComponent);
            var function = contract.GetFunction("getValue");
            
            try
            {
                var result = await function.CallAsync<PositionDto>((BigInteger)entity);
                
                Debug.Log($"Position is {result.X},{result.Y}");
            }
            catch (SmartContractRevertException revert)
            {
                Debug.Log($"Revert happened: {revert.RevertMessage}");
            }
        }
        
        [Button]
        private async void HasAccess()
        {
            await Web.Ecs.HasAccess(movementSystem, positionComponent);
        }
    }
}