using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Context;
using Dtos;
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

        [ShowInInspector] private List<int> steps;

        private IAccount Account => EditorContext.Instance.Account;
        private MyWeb Web => EditorContext.Instance.Web;
        
        private int Gas => 10000000;
        
        [Button]
        private async void Move(int entity)
        {
            var contract = Web.GetContract(movementSystem);
            var function = contract.GetFunction("executeTyped");
            var stepBytes = steps.Select(step => (byte)step).ToArray();
            var receipt = await function.ExecuteAsync(Account, Gas, entity, stepBytes);
            
            Debug.Log($"Moved. Status {receipt.Status}. Gas used {receipt.GasUsed}.");
        }
        
        [Button]
        private async void GetPosition(int entity)
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