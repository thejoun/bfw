using Core;
using Dtos;
using Interfaces;
using Objects;
using Serializables.General;
using Zenject;

namespace Installers
{
    public class EventInstaller
    {
        public static void InstallInto(DiContainer container)
        {
            var entityComponentValueSet = new FilteredEvent<byte[], EntityAddressFilter, ComponentValueSetEventDto>();
            
            container
                .Bind<IFilteredRaisable<byte[], ComponentValueSetEventDto>>()
                .WithId(EventID.EntityComponentValueSet)
                .FromInstance(entityComponentValueSet);
            
            container
                .Bind<IFilteredListenable<byte[], EntityAddressFilter>>()
                .WithId(EventID.EntityComponentValueSet)
                .FromInstance(entityComponentValueSet);
        }
    }
}