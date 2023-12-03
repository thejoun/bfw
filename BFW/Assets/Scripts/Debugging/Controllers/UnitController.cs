using System;
using System.Threading.Tasks;
using Extensions;
using Interfaces;
using Sirenix.OdinInspector;
using Structs;
using Unimorph.Fields;
using UnityEngine;

namespace Contracts.Controllers
{
    [Serializable]
    public class UnitController : ContractController
    {
        [SerializeField] private Reference<IContract> unitSpawnSystem;

        [Button]
        private async void Spawn(int entity, int archetype, Vector2Int position)
        {
            await SpawnUnit(entity, archetype, position);
        }

        public async Task SpawnUnit(int entity, int archetype, Vector2Int position)
        {
            var contract = Web.GetContract(unitSpawnSystem.Value);
            var function = contract.GetFunction("executeTyped");
            var receipt = await function.ExecuteAsync(Account, GasLimit,
                entity, archetype, position.x, position.y);

            LogReceipt(receipt, "Unit spawn");
        }
    }
}