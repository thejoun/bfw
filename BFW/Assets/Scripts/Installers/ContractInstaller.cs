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
        [HideReferenceObjectPicker] [SerializeReference] public IAccount account;
        [HideReferenceObjectPicker] [SerializeReference] public INode node;
        [SerializeField] public Reference<IContract> worldContract;
        
        [Header("Entities")]
        [HideReferenceObjectPicker] [SerializeReference] private GameObject unitTemplate;
        [HideReferenceObjectPicker] [SerializeReference] private GameObject tileTemplate;
        
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
            EventInstaller.InstallInto(Container);

            // core
            Container.Bind<IEcsWeb>().FromInstance(new EcsWeb(account, node));
            Container.Bind<IAccount>().FromInstance(account);
            
            // managers
            Container.Bind<IContract>().FromInstance(worldContract.Value).WhenInjectedInto<WorldEventManager>();
            
            // ecs
            Container.Bind<IEntity>().FromComponentSibling();

            // entities
            Container.Bind<GameObject>().FromInstance(unitTemplate).WhenInjectedInto<UnitSpawnSystem>();
            Container.Bind<GameObject>().FromInstance(tileTemplate).WhenInjectedInto<TileSpawnSystem>();
            
            // components
            Container.Bind<IContract>().FromInstance(positionComponent).WhenInjectedInto<PositionComponent>();
            Container.Bind<IContract>().FromInstance(terrainComponent).WhenInjectedInto<TerrainComponent>();
            Container.Bind<IContract>().FromInstance(archetypeComponent).WhenInjectedInto<ArchetypeComponent>();
            
            // systems
            Container.Bind<IContract>().FromInstance(movementSystem).WhenInjectedInto<MovementSystem>();
            Container.Bind<IContract>().FromInstance(unitSpawnSystem).WhenInjectedInto<UnitSpawnSystem>();
            Container.Bind<IContract>().FromInstance(tileSpawnSystem).WhenInjectedInto<TileSpawnSystem>();
            
            // misc
            Container.Bind<SpriteRenderer>().FromComponentInChildren().WhenInjectedInto<TerrainComponent>();
            Container.Bind<SpriteRenderer>().FromComponentInChildren().WhenInjectedInto<ArchetypeComponent>();
        }
    }
}