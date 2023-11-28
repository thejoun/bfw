using Const;
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
        
        [Inject(Id = ID.EntityParentTransform)] private Transform entityParent;
        
        [Button] [HideInEditorMode]
        public void Execute(int entityId, int terrainId, Vector2Int position)
        {
            ExecuteLocal(entityId, terrainId, position);
            ExecuteRemote(entityId, terrainId, position);
        }

        private void ExecuteLocal(int entityId, int terrainId, Vector2Int position)
        {
            var entity = instantiator.InstantiateComponentOnNewGameObject<Entity>().WithId(entityId);
            
            var instance = entity.GameObject;
            instance.name = $"Entity {entityId}";
            instance.transform.SetParent(entityParent);

            using (new Inactive(instance))
            {
                instantiator.InstantiateComponent<TerrainComponent>(instance).WithValue(terrainId);
                instantiator.InstantiateComponent<PositionComponent>(instance).WithValue(position);
            }
        }

        private void ExecuteRemote(int entityId, int terrainId, Vector2Int position)
        {
            var webContract = web.GetContract(contract);
            var function = webContract.GetFunction("executeTyped");
            
            function.ExecuteAsync(account, gasLimit, entityId, terrainId, position.x, position.y)
                .WithCallback(OnReceiptReceived);
        }
    }
}