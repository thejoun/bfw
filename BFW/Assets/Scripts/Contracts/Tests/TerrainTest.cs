using System.Threading.Tasks;
using Context;
using Interfaces;
using Objects;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game
{
    public class TerrainTest : MonoBehaviour
    {
        [SerializeReference] private IContract terrainComponent;

        private EcsWeb Web => EditorContext.Instance.Web;
        
        [Button]
        private async void GetTerrain(int entity)
        {
            await CheckTerrain(entity);
        }

        public async Task CheckTerrain(int entity)
        {
            var contract = Web.GetContract(terrainComponent);
            var function = contract.GetFunction("getValue");
            var result = await function.CallAsync<int>(entity);

            Debug.Log($"Terrain for {entity}: {result}");
        }
    }
}