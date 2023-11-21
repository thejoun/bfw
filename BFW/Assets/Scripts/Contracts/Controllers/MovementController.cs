using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Dtos;
using Enums;
using Extensions;
using Interfaces;
using Nethereum.ABI.FunctionEncoding;
using Sirenix.OdinInspector;
using Unimorph.Fields;
using UnityEngine;

namespace Contracts.Controllers
{
    [Serializable]
    public class MovementController : ContractController
    {
        [SerializeField] private Reference<IContract> positionComponent;
        [SerializeField] private Reference<IContract> movementSystem;
        [SerializeField] private Reference<IContract> movementPointsComponent;

        [ShowInInspector] private List<HexDirection> steps;
        
        [Button]
        private async void MoveButton(int entity)
        {
            await Move(entity, steps.ToArray());
        }

        public async Task Move(int entity, params HexDirection[] stepDirections)
        {
            var stepBytes = stepDirections.Select(step => (byte)(int)step).ToArray();

            var contract = Web.GetContract(movementSystem.Value);
            var function = contract.GetFunction("executeTyped");
            var receipt = await function.ExecuteAsync(Account, GasLimit, entity, stepBytes);

            LogReceipt(receipt, "Moved");
        }
        
        [Button]
        public async Task CheckMovementPoints(int entity)
        {
            var contract = Web.GetContract(movementPointsComponent.Value);
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
            var contract = Web.GetContract(positionComponent.Value);
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
            await Web.Ecs.HasAccess(movementSystem.Value, positionComponent.Value);
        }
    }
}