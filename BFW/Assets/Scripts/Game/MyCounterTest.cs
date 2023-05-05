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
    public class MyCounterTest : MonoBehaviour
    {
        [SerializeReference] private IContract myComponent;
        
        private INode Node => EditorContext.Instance.Node;
        private IAccount Account => EditorContext.Instance.Account;

        [Button]
        private void Increment()
        {
            var web3 = Web3Helper.CreateWeb3(Node, Account);
            var contract = web3.Eth.GetContract(myComponent.Abi, myComponent.Address);
            var function = contract.GetFunction("change");
            
            var gas = new HexBigInteger(100000);
            
            var task = function.SendTransactionAndWaitForReceiptAsync(Account.Address, gas, 
                null, null, 
                1);
            task.Wait();
            
            Debug.Log("Increment success");
        }

        [Button]
        private void Check()
        {
            var web3 = Web3Helper.CreateWeb3(Node, Account);
            var contract = web3.Eth.GetContract(myComponent.Abi, myComponent.Address);
            var function = contract.GetFunction("check");
            
            var task = function.CallAsync<int>();
            task.Wait();
            var result = task.Result;

            Debug.Log($"Counter is {result}");
        }
    }
}