using Microsoft.Extensions.DependencyInjection;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events.Item;
using OxHack.Inventory.Query.Handlers;

namespace OxHack.Inventory.Query
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterQueryModelEventHandlers(this IServiceCollection @this)
        {
			@this.AddSingleton<ItemQueryModelUpdater>();

            var provider = @this.BuildServiceProvider();

            var bus = provider.GetService<IBus>();
            var handler = provider.GetService<ItemQueryModelUpdater>();

            bus.RegisterEventHandler<ItemCreated>(handler);
            bus.RegisterEventHandler<NameChanged>(handler);
        }
    }
}
