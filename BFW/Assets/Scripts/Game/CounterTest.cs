using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Context;
using Dtos;
using Helpers;
using Interfaces;
using Nethereum.Hex.HexTypes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class CounterTest : MonoBehaviour
    {
        [SerializeReference] private IContract incrementSystem;
        [SerializeReference] private IContract counterComponent;
        
        private INode Node => EditorContext.Instance.Node;
        private IAccount Account => EditorContext.Instance.Account;

        [Button]
        private void IncrementCounter(int entity)
        {
            var web3 = Web3Helper.CreateWeb3(Node, Account);
            var contract = web3.Eth.GetContract(incrementSystem.Abi, incrementSystem.Address);
            var function = contract.GetFunction("executeTyped");
            
            var gas = new HexBigInteger(100000);
            
            var task = function.SendTransactionAndWaitForReceiptAsync(Account.Address, gas, 
                null, null, 
                entity);
            task.Wait();
            
            Debug.Log("Increment sent");
        }

        [Button]
        private void CheckCounter(int entity)
        {
            var web3 = Web3Helper.CreateWeb3(Node, Account);
            var contract = web3.Eth.GetContract(counterComponent.Abi, counterComponent.Address);
            var function = contract.GetFunction("getValue");
            
            var task = function.CallAsync<int>(entity);
            task.Wait();
            var result = task.Result;

            Debug.Log($"Counter is {result}");
        }

        [Button]
        private void GetEntities()
        {
            var web3 = Web3Helper.CreateWeb3(Node, Account);
            var contract = web3.Eth.GetContract(counterComponent.Abi, counterComponent.Address);
            var function = contract.GetFunction("getEntities");
            
            var task = function.CallAsync<List<int>>();
            task.Wait();
            var result = task.Result;

            if (result/*.Values*/ == null)
            {
                Debug.Log("Entity values are null");
                return;
            }
            
            var values = result/*.Values*/ ?? Enumerable.Empty<int>();

            Debug.Log($"Entities are: {string.Join(',', values)}");
        }
    }
}