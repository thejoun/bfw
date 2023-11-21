using ECS.Components;
using ECS.Entities;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ECS.Systems
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
            var instance = instantiator.InstantiatePrefab(template);
            
            instance.name = $"Entity {entity} (tile)";
            instance.transform.SetParent(transform);

            var e = instantiator.InstantiateComponent<Entity>(instance);

            var tc = instantiator.InstantiateComponent<TerrainComponent>(instance);
            var pc = instantiator.InstantiateComponent<PositionComponent>(instance);

            e.SetId(entity);

            tc.SetValue(terrain);
            pc.SetValue(position);
        }
    }
}