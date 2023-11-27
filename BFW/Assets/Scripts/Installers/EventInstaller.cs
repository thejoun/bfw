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
            var componentValueSet = new Event<ComponentValueSetPayload>();

            container.Bind<IRaisable<ComponentValueSetPayload>>()
                .WithId(ID.ComponentValueSetEvent)
                .FromInstance(componentValueSet);
            
            container.Bind<IListenable<ComponentValueSetPayload>>()
                .WithId(ID.ComponentValueSetEvent)
                .FromInstance(componentValueSet);

            var filteredComponentValueSet = new FilteredEvent<byte[], EntityAddressFilter, ComponentValueSetPayload>();
            
            container.Bind<IFilteredRaisable<byte[], ComponentValueSetPayload>>()
                .WithId(ID.ComponentValueSetFilteredEvent)
                .FromInstance(filteredComponentValueSet);
            
            container.Bind<IFilteredListenable<byte[], EntityAddressFilter>>()
                .WithId(ID.ComponentValueSetFilteredEvent)
                .FromInstance(filteredComponentValueSet);
        }
    }
}