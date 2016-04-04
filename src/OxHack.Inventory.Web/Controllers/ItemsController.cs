using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OxHack.Inventory.Data.Models;
using OxHack.Inventory.Services;

namespace OxHack.Inventory.Web.Controllers
{
	[Route("api/[controller]")]
	public class ItemsController : Controller
	{
		private readonly ItemService itemService;

		public ItemsController(ItemService itemService)
		{
			this.itemService = itemService;
		}

		[HttpGet]
		public async Task<IEnumerable<Item>> GetAll()
		{
			return await this.itemService.GetAllItemsAsync();
		}
	}
}
