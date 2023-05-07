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
        [HideReferenceObjectPicker] 
        [SerializeReference] public IAccount account;

        [HideReferenceObjectPicker] 
        [SerializeReference] public INode node;
        
        [Header("Systems")]
        
        [HideReferenceObjectPicker] 
        [SerializeReference] private IContract movementSystem;
        
        [HideReferenceObjectPicker] 
        [SerializeReference] private IContract unitSpawnSystem;
        
        [Header("Components")]
        
        [HideReferenceObjectPicker] 
        [SerializeReference] private IContract positionComponent;

        public override void InstallBindings()
        {
            var web = new EcsWeb(account, node);

            // Container.Bind<IInstantiator>().FromInstance(Container);
            
            Container.Bind<IEcsWeb>().FromInstance(web);
            Container.Bind<IAccount>().FromInstance(account);

            // config
            Container.Bind<HexBigInteger>().WithId("GasLimit").FromInstance(new HexBigInteger(10000000));
            
            // systems
            Container.Bind<IContract>().FromInstance(movementSystem).WhenInjectedInto<MovementSystem>();
            Container.Bind<IContract>().FromInstance(unitSpawnSystem).WhenInjectedInto<UnitSpawnSystem>();
            
            // components
            Container.Bind<IContract>().FromInstance(positionComponent).WhenInjectedInto<PositionComponent>();
        }
    }
}