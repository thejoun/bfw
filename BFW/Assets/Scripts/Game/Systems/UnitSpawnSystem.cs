using Extensions;
using Game.Components;
using Game.Entities;
using Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Systems
{
    public class UnitSpawnSystem : EcsSystem
    {
        [Inject] private IContract contract;
        [Inject] private IInstantiator instantiator;

        [Button] [HideInEditorMode]
        public void Execute(int entity, int archetype, Vector2Int position)
        {
            ExecuteRemote(entity, archetype, position);
            ExecuteLocal(entity, archetype, position);
        }

        private void ExecuteRemote(int entity, int archetype, Vector2Int position)
        {
            var webContract = web.GetContract(contract);
            var function = webContract.GetFunction("executeTyped");
            
            function.ExecuteAsync(account, gasLimit, entity, archetype, position.x, position.y)
                .WithCallback(OnReceiptReceived);
        }

        private void ExecuteLocal(int entity, int archetype, Vector2Int position)
        {
            var go = new GameObject($"Entity {entity} (unit)");
            go.transform.SetParent(transform);

            var ue = instantiator.InstantiateComponent<Entity>(go);
            var pc = instantiator.InstantiateComponent<PositionComponent>(go);
            var ms = instantiator.InstantiateComponent<MovementSystem>(go);
            
            ue.SetId(entity);
            pc.SetValue(position);
        }
    }
}