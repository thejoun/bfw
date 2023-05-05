using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Context;
using Extensions;
using Helpers;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class WorldTest : MonoBehaviour
    {
        [SerializeReference] private IContract world;

        private INode Node => EditorContext.Instance.Node;
        private IAccount Account => EditorContext.Instance.Account;
        private IContract Uint256Abi => EditorContext.Instance.Uint256Abi;
        
        [Button]
        private void GetComponents()
        {
            var web3 = Web3Helper.CreateWeb3(Node, Account);
            var contract = web3.Eth.GetContract(world.Abi, world.Address);
            var function = contract.GetFunction("components");
            
            var task = function.CallAsync<string>();
            task.Wait();
            var resultAddress = task.Result;

            Debug.Log($"Address of components repository is: {resultAddress}");
            
            var contracts = web3.Eth.GetContract(Uint256Abi.Abi, resultAddress);
            var valuesFunction = contracts.GetFunction("getEntities");
            
            var entitiesTask = valuesFunction.CallAsync<List<BigInteger>>();
            entitiesTask.Wait();
            var result = entitiesTask.Result;

            if (result == null)
            {
                Debug.LogError("Result is null");
                return;
            }
            
            var hexAddresses = result.Select(address => address.ToHexString());
            
            Debug.Log($"Registered components:\n{string.Join('\n', hexAddresses)}\n");
        }
        
        [Button]
        private void GetSystems()
        {
            var web3 = Web3Helper.CreateWeb3(Node, Account);
            var contract = web3.Eth.GetContract(world.Abi, world.Address);
            var function = contract.GetFunction("systems");
            
            var task = function.CallAsync<string>();
            task.Wait();
            var resultAddress = task.Result;

            Debug.Log($"Address of systems repository is: {resultAddress}");
            
            var contracts = web3.Eth.GetContract(Uint256Abi.Abi, resultAddress);
            var valuesFunction = contracts.GetFunction("getEntities");
            
            var entitiesTask = valuesFunction.CallAsync<List<BigInteger>>();
            entitiesTask.Wait();
            var result = entitiesTask.Result;

            if (result == null)
            {
                Debug.LogError("Result is null");
                return;
            }

            var hexAddresses = result.Select(address => address.ToHexString());
            
            Debug.Log($"Registered systems:\n{string.Join('\n', hexAddresses)}\n");
        }
    }
}