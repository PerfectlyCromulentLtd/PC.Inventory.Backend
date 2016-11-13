using Microsoft.Extensions.DependencyInjection;
using OxHack.Inventory.Command.Handlers;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Commands.Item;
using OxHack.Inventory.Query.Repositories;

namespace OxHack.Inventory.Services
{
    public static class StartupExtensions
    {
        public static void RegisterCommandHandlers(this IServiceCollection @this)
        {
            var provider = @this.BuildServiceProvider();

            var eventStore = provider.GetService<IEventStore>();
            var photoRepo = provider.GetService<IPhotoRepository>();

            var itemHandler = new ItemCommandHandler(eventStore);
            var photoHandler = new PhotoCommandHandler(eventStore, photoRepo);

			var bus = provider.GetService<IBus>();

			bus.RegisterCommandHandler<CreateItemCommand>(itemHandler);
            bus.RegisterCommandHandler<ChangeAdditionalInformationCommand>(itemHandler);
            bus.RegisterCommandHandler<ChangeAppearanceCommand>(itemHandler);
            bus.RegisterCommandHandler<ChangeAssignedLocationCommand>(itemHandler);
            bus.RegisterCommandHandler<ChangeCategoryCommand>(itemHandler);
            bus.RegisterCommandHandler<ChangeCurrentLocationCommand>(itemHandler);
            bus.RegisterCommandHandler<ChangeIsLoanCommand>(itemHandler);
            bus.RegisterCommandHandler<ChangeManufacturerCommand>(itemHandler);
            bus.RegisterCommandHandler<ChangeModelCommand>(itemHandler);
            bus.RegisterCommandHandler<ChangeNameCommand>(itemHandler);
            bus.RegisterCommandHandler<ChangeOriginCommand>(itemHandler);
            bus.RegisterCommandHandler<ChangeQuantityCommand>(itemHandler);
            bus.RegisterCommandHandler<ChangeSpecCommand>(itemHandler);
            bus.RegisterCommandHandler<UpdateItemCommand>(itemHandler);

            bus.RegisterCommandHandler<AddPhotoCommand>(photoHandler);
            bus.RegisterCommandHandler<RemovePhotoCommand>(photoHandler);
        }
    }
}
