using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Context;
using Dtos;
using Extensions;
using Helpers;
using Interfaces;
using Sirenix.OdinInspector;
using Unimorph.Fields;
using UnityEngine;

namespace Contracts.Controllers
{
    [Serializable]
    public class WorldController : ContractController
    {
        [SerializeField] private Reference<IContract> world;

        private IContract World => world.Value;

        private static INode Node => EditorContext.Instance.Node;
        private static IContract Uint256Abi => EditorContext.Instance.Uint256Abi;

        [Button]
        private async void GetComponentValueSetLog()
        {
            var web3 = Web3Helper.CreateWeb3(Node, Account);
            var contract = web3.Eth.GetContract(World.Abi, World.AddressHex);
            var ev = contract.GetEvent("ComponentValueSet");

            var filterAll = await ev.CreateFilterAsync();

            try
            {
                var eventLogList = await ev.GetAllChangesAsync<ComponentValueSetPayload>(filterAll);
                
                var count = eventLogList.Count;
            
                var textLogs = eventLogList.Select(log => log.Event.ToString());
                var log = string.Join("\n", textLogs);
            
                Debug.Log($"ComponentValueSet: {count} logs\n{log}");
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        
        [Button]
        private void GetComponents()
        {
            var web3 = Web3Helper.CreateWeb3(Node, Account);
            var contract = web3.Eth.GetContract(World.Abi, World.AddressHex);
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
            var contract = web3.Eth.GetContract(World.Abi, World.AddressHex);
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