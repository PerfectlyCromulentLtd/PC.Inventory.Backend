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
		Task<Item> GetItemByIdAsync(Guid id);
		Task<IEnumerable<Item>> GetItemsByCategoryAsync(string category);
		Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
	}
}
