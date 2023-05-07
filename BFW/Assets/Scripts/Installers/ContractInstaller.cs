using Core;
using Game.Components;
using Game.Systems;
using Interfaces;
using Nethereum.Hex.HexTypes;
using Objects;
using Sirenix.OdinInspector;
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
            var web = new EcsWeb(account, node);

            // core
            Container.Bind<IEcsWeb>().FromInstance(web);
            Container.Bind<IAccount>().FromInstance(account);

            // config
            Container.Bind<HexBigInteger>().WithId("GasLimit").FromInstance(new HexBigInteger(10000000));

            // entities
            Container.Bind<GameObject>().FromInstance(unitTemplate).WhenInjectedInto<UnitSpawnSystem>();
            Container.Bind<GameObject>().FromInstance(tileTemplate).WhenInjectedInto<TileSpawnSystem>();
            
            // components
            Container.Bind<IContract>().FromInstance(positionComponent).WhenInjectedInto<PositionComponent>();
            Container.Bind<IContract>().FromInstance(terrainComponent).WhenInjectedInto<TerrainComponent>();
            
            // systems
            Container.Bind<IContract>().FromInstance(movementSystem).WhenInjectedInto<MovementSystem>();
            Container.Bind<IContract>().FromInstance(unitSpawnSystem).WhenInjectedInto<UnitSpawnSystem>();
            Container.Bind<IContract>().FromInstance(tileSpawnSystem).WhenInjectedInto<TileSpawnSystem>();
            
            
            
            // misc
            Container.Bind<IEntity>().FromComponentSibling();
            Container.Bind<SpriteRenderer>().FromComponentInChildren().WhenInjectedInto<TerrainComponent>();
        }
    }
}