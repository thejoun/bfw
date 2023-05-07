using System.Threading.Tasks;
using Context;
using Extensions;
using Interfaces;
using Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class UnitTest : MonoBehaviour
    {
        [SerializeReference] private IContract unitSpawnSystem;

        private EcsWeb Web => EditorContext.Instance.Web;
        private IAccount Account => EditorContext.Instance.Account;
        
        private int Gas => 10000000;
        
        [Button]
        private async void Spawn(int entity, int archetype, Vector2Int position)
        {
            await SpawnUnit(entity, archetype, position);
        }

        public async Task SpawnUnit(int entity, int archetype, Vector2Int position)
        {
            var contract = Web.GetContract(unitSpawnSystem);
            var function = contract.GetFunction("executeTyped");

            var receipt = await function.ExecuteAsync(Account, Gas,
                entity, archetype, position.x, position.y);

            Debug.Log($"Unit spawn: Status {receipt.Status}; Gas used {receipt.GasUsed}.");
        }
    }
}