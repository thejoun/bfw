using Extensions;
using Game.Components;
using Game.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Systems
{
    public class TileSpawnSystem : EcsSystem
    {
        [Inject] private IInstantiator instantiator;
        [Inject] private GameObject template;

        [Button] [HideInEditorMode]
        public void Execute(int entity, int terrain, Vector2Int position)
        {
            ExecuteRemote(entity, terrain, position);
            ExecuteLocal(entity, terrain, position);
        }

        private void ExecuteRemote(int entity, int terrain, Vector2Int position)
        {
            var webContract = web.GetContract(contract);
            var function = webContract.GetFunction("executeTyped");
            
            function.ExecuteAsync(account, gasLimit, entity, terrain, position.x, position.y)
                .WithCallback(OnReceiptReceived);
        }

        private void ExecuteLocal(int entity, int terrain, Vector2Int position)
        {
            var go = instantiator.InstantiatePrefab(template);
            go.name = $"Entity {entity} (tile)";
            go.transform.SetParent(transform);

            var e = instantiator.InstantiateComponent<Entity>(go);

            var tc = instantiator.InstantiateComponent<TerrainComponent>(go);
            var pc = instantiator.InstantiateComponent<PositionComponent>(go);

            e.SetId(entity);

            tc.SetValue(terrain);
            pc.SetValue(position);
        }
    }
}