using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Commands;
using OxHack.Inventory.Cqrs.Commands.Item;
using OxHack.Inventory.Cqrs.Events;
using OxHack.Inventory.Cqrs.Events.Item;
using System;
using System.Collections.Generic;
using System.Linq;
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
        IHandle<ChangeSpecCommand>
    {
        private readonly IEventStore eventStore;

        public ItemCommandHandler(IEventStore eventStore)
        {
            this.eventStore = eventStore;
        }

        public async Task Handle(CreateItemCommand message)
        {
            this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeAdditionalInformationCommand message)
        {
            this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeAppearanceCommand message)
        {
            this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeAssignedLocationCommand message)
        {
            this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeCategoryCommand message)
        {
            this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeCurrentLocationCommand message)
        {
            this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeIsLoanCommand message)
        {
            this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeManufacturerCommand message)
        {
            this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeModelCommand message)
        {
            this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeNameCommand message)
        {
            this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeOriginCommand message)
        {
            this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeQuantityCommand message)
        {
            this.PublishMappedEvent(message);
        }

        public async Task Handle(ChangeSpecCommand message)
        {
            this.PublishMappedEvent(message);
        }

        private void PublishMappedEvent<TEvent>(IMapToEvent<TEvent> source) where TEvent : IEvent
        {
            var @event = source.GetEvent();
			this.eventStore.StoreEvent(@event);
        }
    }
}
