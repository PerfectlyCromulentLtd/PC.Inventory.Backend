using Microsoft.Extensions.DependencyInjection;
using OxHack.Inventory.Command.Handlers;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Commands.Item;

namespace OxHack.Inventory.Services
{
    public static class StartupExtensions
    {
        public static void RegisterCommandHandlers(this IServiceCollection @this)
        {
            var provider = @this.BuildServiceProvider();

            var bus = provider.GetService<IBus>();
            var handler = new ItemCommandHandler(bus);

            bus.RegisterCommandHandler<CreateItemCommand>(handler);
            bus.RegisterCommandHandler<ChangeAdditionalInformationCommand>(handler);
            bus.RegisterCommandHandler<ChangeAppearanceCommand>(handler);
            bus.RegisterCommandHandler<ChangeAssignedLocationCommand>(handler);
            bus.RegisterCommandHandler<ChangeCategoryCommand>(handler);
            bus.RegisterCommandHandler<ChangeCurrentLocationCommand>(handler);
            bus.RegisterCommandHandler<ChangeIsLoanCommand>(handler);
            bus.RegisterCommandHandler<ChangeManufacturerCommand>(handler);
            bus.RegisterCommandHandler<ChangeModelCommand>(handler);
            bus.RegisterCommandHandler<ChangeNameCommand>(handler);
            bus.RegisterCommandHandler<ChangeOriginCommand>(handler);
            bus.RegisterCommandHandler<ChangeQuantityCommand>(handler);
            bus.RegisterCommandHandler<ChangeSpecCommand>(handler);
        }
    }
}
