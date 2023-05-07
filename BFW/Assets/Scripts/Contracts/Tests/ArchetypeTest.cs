using System.Collections.Generic;
using System.Threading.Tasks;
using Context;
using Extensions;
using Interfaces;
using Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class ArchetypeTest : MonoBehaviour
    {
        [SerializeReference] private IContract terrainMovementCostComponent;

        [SerializeField] private List<Vector2Int> terrainCost;
        
        private MyWeb Web => EditorContext.Instance.Web;
        private IAccount Account => EditorContext.Instance.Account;
        
        private int Gas => 10000000;
        
        [Button]
        private async void Create(int entity)
        {
            await CreateArchetype(entity, terrainCost.ToArray());
        }

        public async Task CreateArchetype(int entity, params Vector2Int[] terrainCosts)
        {
            var contract = Web.GetContract(terrainMovementCostComponent);
            var function = contract.GetFunction("set");

            foreach (var terrain in terrainCosts)
            {
                var receipt = await function.ExecuteAsync(Account, Gas,
                    entity, terrain.x, terrain.y);

                Debug.Log($"Set terrain cost: Status {receipt.Status}; Gas used {receipt.GasUsed}.");
            }
        }
    }
}