using Context;
using Extensions;
using Interfaces;
using Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class TileTest : MonoBehaviour
    {
        [SerializeReference] private IContract tileSpawnSystem;

        private IAccount Account => EditorContext.Instance.Account;
        private MyWeb Web => EditorContext.Instance.Web;
        
        private int Gas => 10000000;
        
        [Button]
        private async void Spawn(int entity, int terrain, Vector2Int position)
        {
            var contract = Web.GetContract(tileSpawnSystem);
            var function = contract.GetFunction("executeTyped");
            var receipt = await function.ExecuteAsync(Account, Gas, 
                entity, terrain, position.x, position.y);
            
            Debug.Log($"Tile spawn: Status {receipt.Status}; Gas used {receipt.GasUsed}.");
        }
    }
}