using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace ECS.Systems
{
    public class UnitSpawnSystem : EcsSystem
    {
        [Inject] private IInstantiator instantiator;

        [Button] [HideInEditorMode]
        public void Execute(int entityId, int archetypeId, Vector2Int position)
        {
            ExecuteLocal(entityId, archetypeId, position);
            ExecuteRemote(entityId, archetypeId, position);
        }

        private void ExecuteLocal(int entityId, int archetypeId, Vector2Int position)
        {
            // var entity = instantiator.InstantiateComponentOnNewGameObject<Entity>().WithId(entityId);
            //
            // var instance = entity.GameObject;
            // instance.name = $"Entity {entityId}";
            // instance.transform.SetParent(transform);
            //
            // using (new Inactive(instance))
            // {
            //     instantiator.InstantiateComponent<ArchetypeComponent>(instance).WithValue(archetypeId);
            //     instantiator.InstantiateComponent<PositionComponent>(instance).WithValue(position);
            // }
        }

        private void ExecuteRemote(int entityId, int archetypeId, Vector2Int position)
        {
            var webContract = web.GetContract(contract);
            var function = webContract.GetFunction("executeTyped");
            
            function.ExecuteAsync(account, gasLimit, entityId, archetypeId, position.x, position.y)
                .WithCallback(OnReceiptReceived);
        }
    }
}