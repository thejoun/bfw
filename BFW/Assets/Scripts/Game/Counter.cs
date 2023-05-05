using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Context;
using Dtos;
using Helpers;
using Interfaces;
using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class Counter : MonoBehaviour
    {
        [SerializeReference] private IContract incrementSystem;
        [SerializeReference] private IContract counterComponent;
        
        private INode Node => EditorContext.Instance.Node;
        private IAccount Account => EditorContext.Instance.Account;
        
        [Button]
        private void Increment(int entity)
        {
            IncrementCounter(entity);
        }

        [Button]
        private void Check(int entity)
        {
            CheckCounter(entity);
        }
        
        [Button]
        private void Test()
        {
            TestCounter();
        }

        [Button]
        private void Entities()
        {
            GetEntities();
        }
        
        [Button]
        private void Set(int entity, int value)
        {
            SetEntity(entity, value);
        }
        
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
            
            Debug.Log("Increment success");
        }

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
        
        private void TestCounter()
        {
            var web3 = Web3Helper.CreateWeb3(Node, Account);
            var contract = web3.Eth.GetContract(counterComponent.Abi, counterComponent.Address);
            var function = contract.GetFunction("test");
            
            var task = function.CallAsync<string>(0);
            task.Wait();
            var result = task.Result;

            Debug.Log($"Test returned {result}");
        }
        
        private void TestCounter2(int entity)
        {
            var web3 = Web3Helper.CreateWeb3(Node, Account);
            var contract = web3.Eth.GetContract(counterComponent.Abi, counterComponent.Address);
            var function = contract.GetFunction("test2");
            
            var task = function.CallAsync<int>(entity);
            task.Wait();
            var result = task.Result;

            Debug.Log($"Test returned {result}");
        }
        
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
        
        private void SetEntity(int entity, int value)
        {
            var web3 = Web3Helper.CreateWeb3(Node, Account);
            var contract = web3.Eth.GetContract(counterComponent.Abi, counterComponent.Address);
            var function = contract.GetFunctionBySignature("d923c3c4");

            var gas = new HexBigInteger(100000);
            var sentValue = new HexBigInteger(0);
            
            var task = function.SendTransactionAndWaitForReceiptAsync(Account.Address, gas, 
                null, null, 
                entity, value);
            
            task.Wait();
            var result = task.Result;
            
            Debug.Log($"Set entity ({entity}, {value}) success");
        }
    }
}