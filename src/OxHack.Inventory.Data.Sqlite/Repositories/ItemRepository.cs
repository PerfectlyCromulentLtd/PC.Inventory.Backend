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
		public Task<IReadOnlyCollection<Item>> GetAllItemsAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Item> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
