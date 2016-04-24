using Microsoft.Extensions.DependencyInjection;
using OxHack.Inventory.Command.Handlers;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Commands.Item;

namespace OxHack.Inventory.Services
{
    public static class IServiceCollectionExtensions
    {
        public static void RegisterCommandHandlers(this IServiceCollection @this)
        {
            var provider = @this.BuildServiceProvider();

            var bus = provider.GetService<IBus>();
            var handler = new ItemCommandHandler(bus);

            bus.RegisterCommandHandler<CreateItemCommand>(handler);
            bus.RegisterCommandHandler<ChangeNameCommand>(handler);
        }
    }
}
