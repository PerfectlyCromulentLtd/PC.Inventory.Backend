using OxHack.Inventory.Query.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OxHack.Inventory.Query.Models;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Commands;

namespace OxHack.Inventory.Services
{
	public class ItemService
	{
		private readonly IBus bus;
		private readonly IItemRepository itemRepo;

		public ItemService(IItemRepository itemRepo, IBus bus)
		{
			this.itemRepo = itemRepo;
			this.bus = bus;
		}

		public async Task<IEnumerable<Item>> GetAllItemsAsync()
		{
			return await this.itemRepo.GetAllItemsAsync();
		}

		public async Task<Item> GetItemByIdAsync(Guid id)
		{
			return await this.itemRepo.GetByIdAsync(id);
		}

		public async Task IssueCommandAsync<TCommand>(TCommand command) where TCommand : ICommand
		{
			await bus.IssueCommandAsync(command);
		}
	}
}
