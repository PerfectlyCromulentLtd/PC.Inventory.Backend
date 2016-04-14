using OxHack.Inventory.Query.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Repositories
{
	public interface IItemRepository
	{
		Task<IEnumerable<Item>> GetAllItemsAsync();

		Task<Item> GetByIdAsync(Guid id);
	}
}
