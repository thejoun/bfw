using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Extensions;
using Interfaces;
using Sirenix.OdinInspector;
using Unimorph.Fields;
using UnityEngine;

namespace Contracts.Controllers
{
    [Serializable]
    public class ArchetypeController : ContractController
    {
        [SerializeField] private Reference<IContract> terrainMovementCostComponent;

        [SerializeField] private List<Vector2Int> terrainCosts;

        [Button]
        private async void Create(int entity)
        {
            await CreateArchetype(entity, terrainCosts.ToArray());
        }

        public async Task CreateArchetype(int entity, params Vector2Int[] terrainCosts)
        {
            var contract = Web.GetContract(terrainMovementCostComponent.Value);
            var function = contract.GetFunction("set");

            foreach (var terrain in terrainCosts)
            {
                var receipt = await function.ExecuteAsync(Account, GasLimit,
                    entity, terrain.x, terrain.y);

                LogReceipt(receipt, "Set terrain cost");
            }
        }
    }
}