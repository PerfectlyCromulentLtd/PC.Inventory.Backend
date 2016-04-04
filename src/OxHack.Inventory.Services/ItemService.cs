using OxHack.Inventory.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OxHack.Inventory.Data.Models;

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
	}
}
