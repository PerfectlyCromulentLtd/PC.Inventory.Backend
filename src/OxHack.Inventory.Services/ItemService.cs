using OxHack.Inventory.Query.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OxHack.Inventory.Query.Models;

namespace OxHack.Inventory.Services
{
    public class ItemService
    {
		private readonly IItemRepository itemRepo;

		public ItemService(IItemRepository itemRepo)
		{
			this.itemRepo = itemRepo;
		}

		public async Task<IEnumerable<Item>> GetAllItemsAsync()
		{
			return await this.itemRepo.GetAllItemsAsync();
		}

		public async Task<Item> GetItemByIdAsync(Guid id)
		{
			return await this.itemRepo.GetByIdAsync(id);
		}
	}
}
