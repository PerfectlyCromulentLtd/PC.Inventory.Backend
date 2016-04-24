using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Commands.Item;
using OxHack.Inventory.Cqrs.Events.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Command.Handlers
{
    internal class ItemCommandHandler : IHandle<CreateItemCommand>, IHandle<ChangeNameCommand>
    {
        private readonly IBus bus;

        public ItemCommandHandler(IBus bus)
        {
            this.bus = bus;
        }

        public async void Handle(CreateItemCommand message)
        {
            var @event =
                new ItemCreated(
                    message.AggregateRootId,
                    message.AdditionalInformation,
                    message.Appearance,
                    message.AssignedLocation,
                    message.Category,
                    message.CurrentLocation,
                    message.IsLoan,
                    message.Manufacturer,
                    message.Model,
                    message.Name,
                    message.Origin,
                    message.Quantity,
                    message.Spec);

            await this.bus.RaiseEventAsync(@event);
        }

        public async void Handle(ChangeNameCommand message)
        {
            var @event = new NameChanged(message.AggregateRootId, message.ConcurrencyId, message.Name);

            await this.bus.RaiseEventAsync(@event);
        }
    }
}
