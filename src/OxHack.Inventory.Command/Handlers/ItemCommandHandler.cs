using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Commands;
using OxHack.Inventory.Cqrs.Commands.Item;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Events.Item;
using System.Threading.Tasks;

namespace OxHack.Inventory.Command.Handlers
{
    internal class ItemCommandHandler :
        IHandle<CreateItemCommand>,
        IHandle<ChangeAdditionalInformationCommand>,
        IHandle<ChangeAppearanceCommand>,
        IHandle<ChangeAssignedLocationCommand>,
        IHandle<ChangeCategoryCommand>,
        IHandle<ChangeCurrentLocationCommand>,
        IHandle<ChangeIsLoanCommand>,
        IHandle<ChangeManufacturerCommand>,
        IHandle<ChangeModelCommand>,
        IHandle<ChangeNameCommand>,
        IHandle<ChangeOriginCommand>,
        IHandle<ChangeQuantityCommand>,
        IHandle<ChangeSpecCommand>,
        IHandle<UpdateItemCommand>
    {
        private readonly IEventStore eventStore;

        public ItemCommandHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public async Task Handle(CreateItemCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(ChangeAdditionalInformationCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(ChangeAppearanceCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(ChangeAssignedLocationCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(ChangeCategoryCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(ChangeCurrentLocationCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(ChangeIsLoanCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(ChangeManufacturerCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(ChangeModelCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(ChangeNameCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(ChangeOriginCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(ChangeQuantityCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(ChangeSpecCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        public async Task Handle(UpdateItemCommand message)
        {
            await this.StoreAggregateEvent(message, message.IssuerMetadata);
        }

        private async Task StoreAggregateEvent<TEvent>(IMapToEvent<TEvent> source, dynamic issuerMetadata) where TEvent : IAggregateEvent
        {
            var @event = source.GetEvent();
			this.eventStore.StoreAggregateEvent(@event, issuerMetadata);
			await Task.WhenAll();
        }
    }
}
