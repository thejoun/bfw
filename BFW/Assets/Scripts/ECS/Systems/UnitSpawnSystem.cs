using ECS.Components;
using ECS.Entities;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Utilities;
using Zenject;

namespace ECS.Systems
{
    public class UnitSpawnSystem : EcsSystem
    {
        [Inject] private IInstantiator instantiator;
        [Inject] private GameObject template;

        [Button] [HideInEditorMode]
        public void Execute(int entity, int archetype, Vector2Int position)
        {
            ExecuteLocal(entity, archetype, position);
            ExecuteRemote(entity, archetype, position);
        }

        private void ExecuteLocal(int entity, int archetype, Vector2Int position)
        {
            var instance = instantiator.InstantiatePrefab(template);

            using (new Inactive(instance))
            {
                instance.name = $"Entity {entity} (unit)";
                instance.transform.SetParent(transform);

                instantiator.InstantiateComponent<Entity>(instance).WithId(entity);
                instantiator.InstantiateComponent<ArchetypeComponent>(instance).WithValue(archetype);
                instantiator.InstantiateComponent<PositionComponent>(instance).WithValue(position);
                instantiator.InstantiateComponent<MovementSystem>(instance);
            }
        }

        private void ExecuteRemote(int entity, int archetype, Vector2Int position)
        {
            var webContract = web.GetContract(contract);
            var function = webContract.GetFunction("executeTyped");
            
            function.ExecuteAsync(account, gasLimit, entity, archetype, position.x, position.y)
                .WithCallback(OnReceiptReceived);
        }
    }
}