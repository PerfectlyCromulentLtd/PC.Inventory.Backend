using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events.Item;
using OxHack.Inventory.Query.Models;
using OxHack.Inventory.Query.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Handlers
{
    public class ItemQueryModelUpdater : IHandle<ItemCreated>, IHandle<NameChanged>
    {
        private readonly IItemRepository itemRepository;

        public ItemQueryModelUpdater(IItemRepository itemRepository)
        {
            this.itemRepository = itemRepository;
        }

        public async void Handle(ItemCreated message)
        {
            try
            {
                var model =
                    new Item(
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
                        message.Spec,
                        null,
                        Guid.Empty);

                await this.itemRepository.CreateItemAsync(model);
            }
            catch
            {
                // TODO: Log that event replay is needed
                throw;
            }
        }

        public async void Handle(NameChanged message)
        {
            try
            {
                var item = await this.itemRepository.GetByIdAsync(message.AggregateRootId);

                item.ConcurrencyId = message.ConcurrencyId;
                item.Name = message.Name;

                await this.itemRepository.UpdateItemAsync(item);
            }
            catch
            {
                // TODO: Log that event replay is needed
                throw;
            }
        }
    }
}
