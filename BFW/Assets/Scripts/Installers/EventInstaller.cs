using Core;
using Dtos;
using ECS.Components;
using ECS.Systems;
using Interfaces;
using Nethereum.Hex.HexTypes;
using Objects;
using Serializables.General;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = nameof(EventInstaller), 
        menuName = MenuName.Installers + nameof(EventInstaller))]
    public class EventInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IEvent<ComponentValueSetEventDto>>()
                .WithId(EventID.ComponentValueSet)
                .FromInstance(new Event<ComponentValueSetEventDto>());
        }
    }
}