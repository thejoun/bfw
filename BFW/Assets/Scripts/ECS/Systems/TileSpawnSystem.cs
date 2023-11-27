using Core;
using ECS.Components;
using ECS.Entities;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities;
using Zenject;

namespace ECS.Systems
{
    public class TileSpawnSystem : EcsSystem
    {
        [Inject] private IInstantiator instantiator;
        [Inject] private GameObject template;
        
        [Inject(Id = ID.EntityParent)] private Transform entityParent;
            
        [Button] [HideInEditorMode]
        public void Execute(int entity, int terrain, Vector2Int position)
        {
            ExecuteLocal(entity, terrain, position);
            ExecuteRemote(entity, terrain, position);
        }

        private void ExecuteLocal(int entity, int terrain, Vector2Int position)
        {
            var instance = instantiator.InstantiatePrefab(template);

            using (new Inactive(instance))
            {
                instance.name = $"Entity {entity} (tile)";
                instance.transform.SetParent(entityParent);

                instantiator.InstantiateComponent<Entity>(instance).WithId(entity);
                instantiator.InstantiateComponent<TerrainComponent>(instance).WithValue(terrain);
                instantiator.InstantiateComponent<PositionComponent>(instance).WithValue(position);
            }
        }

        private void ExecuteRemote(int entity, int terrain, Vector2Int position)
        {
            var webContract = web.GetContract(contract);
            var function = webContract.GetFunction("executeTyped");
            
            function.ExecuteAsync(account, gasLimit, entity, terrain, position.x, position.y)
                .WithCallback(OnReceiptReceived);
        }
    }
}