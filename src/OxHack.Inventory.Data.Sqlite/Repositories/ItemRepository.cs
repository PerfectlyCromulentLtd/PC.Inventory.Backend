using OxHack.Inventory.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OxHack.Inventory.Data.Models;

namespace OxHack.Inventory.Data.Sqlite.Repositories
{
	public class ItemRepository : IItemRepository
	{
		public async Task<IEnumerable<Item>> GetAllItemsAsync()
		{
			return await Task.FromResult(new[] { new Item(), new Item(), new Item(), new Item(), });
		}

		public async Task<Item> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
