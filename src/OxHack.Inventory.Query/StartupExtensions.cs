using Microsoft.Extensions.DependencyInjection;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events.Item;
using OxHack.Inventory.Query.Handlers;

namespace OxHack.Inventory.Query
{
    public static class StartupExtensions
    {
        public static void RegisterQueryModelEventHandlers(this IServiceCollection @this)
        {
			@this.AddSingleton<ItemQueryModelUpdater>();

            var provider = @this.BuildServiceProvider();

            var bus = provider.GetService<IBus>();
            var handler = provider.GetService<ItemQueryModelUpdater>();

            bus.RegisterEventHandler<ItemCreated>(handler);
            bus.RegisterEventHandler<AdditionalInformationChanged>(handler);
            bus.RegisterEventHandler<AppearanceChanged>(handler);
            bus.RegisterEventHandler<AssignedLocationChanged>(handler);
            bus.RegisterEventHandler<CategoryChanged>(handler);
            bus.RegisterEventHandler<CurrentLocationChanged>(handler);
            bus.RegisterEventHandler<IsLoanChanged>(handler);
            bus.RegisterEventHandler<ManufacturerChanged>(handler);
            bus.RegisterEventHandler<ModelChanged>(handler);
            bus.RegisterEventHandler<NameChanged>(handler);
            bus.RegisterEventHandler<OriginChanged>(handler);
            bus.RegisterEventHandler<QuantityChanged>(handler);
            bus.RegisterEventHandler<SpecChanged>(handler);
        }
    }
}
