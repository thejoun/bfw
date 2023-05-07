using Extensions;
using Game.Components;
using Game.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Systems
{
    public class UnitSpawnSystem : EcsSystem
    {
        [Inject] private IInstantiator instantiator;
        [Inject] private GameObject template;

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
            var go = instantiator.InstantiatePrefab(template);
            go.name = $"Entity {entity} (unit)";
            go.transform.SetParent(transform);

            var ue = instantiator.InstantiateComponent<Entity>(go);

            var ac = instantiator.InstantiateComponent<ArchetypeComponent>(go);
            var pc = instantiator.InstantiateComponent<PositionComponent>(go);

            var ms = instantiator.InstantiateComponent<MovementSystem>(go);
            
            ue.SetId(entity);

            ac.SetValue(archetype);
            pc.SetValue(position);
        }
    }
}