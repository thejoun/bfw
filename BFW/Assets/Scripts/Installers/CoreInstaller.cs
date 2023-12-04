using Const;
using Core;
using Dtos;
using Interfaces;
using Objects;
using Zenject;
using Extensions;

namespace Installers
{
    public class CoreInstaller
    {
        public static void InstallInto(DiContainer container)
        {
            container.Bind<IRaisable<ComponentValueSetPayload>, IListenable<ComponentValueSetPayload>>()
                .WithId(ID.ComponentValueSetEvent).FromInstance(new Event<ComponentValueSetPayload>());

            container.Bind<IFilteredRaisable<byte[], ComponentValueSetPayload>, 
                    IFilteredListenable<byte[], EntityAddressFilter>>()
                .WithId(ID.ComponentValueSetFilteredEvent)
                .FromInstance(new FilteredEvent<byte[], EntityAddressFilter, ComponentValueSetPayload>());

            container.Bind<IRegistry<IEntity>, IEntityRegistry>().WithId(ID.EntityRegistry)
                .FromInstance(new EntityRegistry());
        }
    }
}