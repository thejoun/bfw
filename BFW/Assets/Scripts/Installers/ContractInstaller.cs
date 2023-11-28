using System;
using System.Collections.Generic;
using System.Linq;
using Const;
using Core;
using ECS.Components;
using ECS.Systems;
using Interfaces;
using Managers;
using Objects;
using Sirenix.OdinInspector;
using Unimorph.Fields;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = nameof(ContractInstaller), 
        menuName = MenuName.Installers + nameof(ContractInstaller))]
    public class ContractInstaller : ScriptableObjectInstaller
    {
        [SerializeField] public Reference<IAccount> account;
        [SerializeField] public Reference<INode> node;
        [SerializeField] public Reference<IContract> worldContract;
        
        [Header("Components")]
        [HideReferenceObjectPicker] [SerializeReference] private IContract positionComponent;
        [HideReferenceObjectPicker] [SerializeReference] private IContract terrainComponent;
        [HideReferenceObjectPicker] [SerializeReference] private IContract archetypeComponent;
        [HideReferenceObjectPicker] [SerializeReference] private IContract movementPointsComponent;
        
        [Header("Systems")]
        [HideReferenceObjectPicker] [SerializeReference] private IContract movementSystem;
        [HideReferenceObjectPicker] [SerializeReference] private IContract unitSpawnSystem;
        [HideReferenceObjectPicker] [SerializeReference] private IContract tileSpawnSystem;

        public override void InstallBindings()
        {
            // TODO make gathering them automatic somehow
            // TODO move this elsewhere
            ConfigInstaller.InstallInto(Container);
            CoreInstaller.InstallInto(Container);

            // core
            Container.Bind<IEcsWeb>().FromInstance(new EcsWeb(account.Value, node.Value));
            Container.Bind<IAccount>().FromInstance(account.Value);
            
            // managers
            Container.Bind<IContract>().FromInstance(worldContract.Value).WhenInjectedInto<WorldEventManager>();
            
            // components
            using (var components = new ComponentInstaller(Container))
            {
                components.Bind<PositionComponent>(positionComponent);
                components.Bind<TerrainComponent>(terrainComponent);
                components.Bind<ArchetypeComponent>(archetypeComponent);
            }
            
            // systems
            Container.Bind<IContract>().FromInstance(movementSystem).WhenInjectedInto<MovementSystem>();
            Container.Bind<IContract>().FromInstance(unitSpawnSystem).WhenInjectedInto<UnitSpawnSystem>();
            Container.Bind<IContract>().FromInstance(tileSpawnSystem).WhenInjectedInto<TileSpawnSystem>();
        }

        private class ComponentInstaller : IDisposable
        {
            private DiContainer container;
            private List<(IContract contract, Type type)> instances = new();

            public ComponentInstaller(DiContainer container)
            {
                this.container = container;
            }

            public void Bind<T>(IContract instance)
            {
                container.Bind<IContract>().FromInstance(instance).WhenInjectedInto<T>();
                
                instances.Add((instance, typeof(T)));
            }
            
            public void Dispose()
            {
                var dictionary = instances
                    .ToDictionary(instance => 
                        instance.contract.AddressHex, 
                        instance => instance.type);

                container
                    .Bind<IDictionary<string, Type>>()
                    .WithId(ID.AddressComponentDictionary)
                    .FromInstance(dictionary);
            }
        }
    }
}