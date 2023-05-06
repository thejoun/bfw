using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Context;
using Extensions;
using Interfaces;
using Nethereum.ABI.FunctionEncoding;
using Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class CounterTest : MonoBehaviour
    {
        [SerializeReference] private IContract incrementSystem;
        [SerializeReference] private IContract counterComponent;
        
        private IAccount Account => EditorContext.Instance.Account;
        private MyWeb Web => EditorContext.Instance.Web;
        
        private int Gas => 10000000;

        [Button]
        private async void IncrementCounter(int entity)
        {
            var contract = Web.GetContract(incrementSystem);
            var function = contract.GetFunction("executeTyped");
            var receipt = await function.ExecuteAsync(Account, Gas, entity);
            
            Debug.Log($"Incremented. Status {receipt.Status}. Gas used {receipt.GasUsed}.");
        }
        
        [Button]
        private async void SetCounter(int entity, int number)
        {
            var contract = Web.GetContract(incrementSystem);
            var function = contract.GetFunction("setNumber");
            var receipt = await function.ExecuteAsync(Account, Gas, 
                entity, counterComponent.Address, number);
            
            Debug.Log($"Number set. Status {receipt.Status}. Gas used {receipt.GasUsed}.");
        }

        [Button]
        private async void CheckCounter(int entity)
        {
            var contract = Web.GetContract(counterComponent);
            var function = contract.GetFunction("getValue");
            
            try
            {
                var result = await function.CallAsync<int>((BigInteger)entity);
                
                Debug.Log($"Counter is {result}");
            }
            catch (SmartContractRevertException revert)
            {
                Debug.Log($"Revert happened: {revert.RevertMessage}");
            }
        }

        [Button]
        private async void GetEntities()
        {
            var contract = Web.GetContract(counterComponent);
            var function = contract.GetFunction("getEntities");
            var result = await function.CallAsync<List<int>>();
            var values = result ?? Enumerable.Empty<int>();
            var stringValues = values.Select(val => val.ToString());

            Debug.Log($"Entities are: {string.Join(',', stringValues)}");
        }
        
        [Button]
        private async void HasAccess()
        {
            await Web.Ecs.HasAccess(incrementSystem, counterComponent);
        }
    }
}