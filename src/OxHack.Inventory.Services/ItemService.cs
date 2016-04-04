using OxHack.Inventory.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Services
{
    public class ItemService
    {
		private readonly IItemRepository itemRepo;

		public ItemService(IItemRepository itemRepo)
		{
			this.itemRepo = itemRepo;
		}
    }
}
