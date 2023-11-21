using System;
using System.Threading.Tasks;
using Extensions;
using Interfaces;
using Sirenix.OdinInspector;
using Unimorph.Fields;
using UnityEngine;

namespace Contracts.Controllers
{
    [Serializable]
    public class TileController : ContractController
    {
        [SerializeField] private Reference<IContract> tileSpawnSystem;
        
        [Button]
        private async void Spawn(int entity, int terrain, Vector2Int position)
        {
            await SpawnTile(entity, terrain, position);
        }

        public async Task SpawnTile(int entity, int terrain, Vector2Int position)
        {
            var contract = Web.GetContract(tileSpawnSystem.Value);
            var function = contract.GetFunction("executeTyped");
            var receipt = await function.ExecuteAsync(Account, GasLimit,
                entity, terrain, position.x, position.y);

            LogReceipt(receipt, "Tile spawn");
        }
    }
}