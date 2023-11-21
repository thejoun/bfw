using System;
using System.Threading.Tasks;
using Interfaces;
using Sirenix.OdinInspector;
using Unimorph.Fields;
using UnityEngine;

namespace Contracts.Controllers
{
    [Serializable]
    public class TerrainController : ContractController
    {
        [SerializeField] private Reference<IContract> terrainComponent;
        
        [Button]
        private async void GetTerrain(int entity)
        {
            await CheckTerrain(entity);
        }

        public async Task CheckTerrain(int entity)
        {
            var contract = Web.GetContract(terrainComponent.Value);
            var function = contract.GetFunction("getValue");
            var result = await function.CallAsync<int>(entity);

            Debug.Log($"Terrain for entity {entity} is '{result}'");
        }
    }
}