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
            var itemHandler = provider.GetService<ItemQueryModelUpdater>();

			bus.RegisterEventHandler<ItemCreated>(itemHandler);
			bus.RegisterEventHandler<AdditionalInformationChanged>(itemHandler);
            bus.RegisterEventHandler<AppearanceChanged>(itemHandler);
            bus.RegisterEventHandler<AssignedLocationChanged>(itemHandler);
            bus.RegisterEventHandler<CategoryChanged>(itemHandler);
            bus.RegisterEventHandler<CurrentLocationChanged>(itemHandler);
            bus.RegisterEventHandler<IsLoanChanged>(itemHandler);
            bus.RegisterEventHandler<ManufacturerChanged>(itemHandler);
            bus.RegisterEventHandler<ModelChanged>(itemHandler);
            bus.RegisterEventHandler<NameChanged>(itemHandler);
            bus.RegisterEventHandler<OriginChanged>(itemHandler);
            bus.RegisterEventHandler<QuantityChanged>(itemHandler);
            bus.RegisterEventHandler<SpecChanged>(itemHandler);
			bus.RegisterEventHandler<PhotoAdded>(itemHandler);
			bus.RegisterEventHandler<PhotoRemoved>(itemHandler);
		}
    }
}
