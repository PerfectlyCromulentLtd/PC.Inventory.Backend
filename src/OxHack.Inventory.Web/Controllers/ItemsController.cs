using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OxHack.Inventory.Services;
using OxHack.Inventory.Web.Models;
using OxHack.Inventory.Web.Extensions;
using Microsoft.Extensions.Configuration;
using OxHack.Inventory.Web.Filters;

namespace OxHack.Inventory.Web.Controllers
{
	[Route("api/[controller]")]
	public class ItemsController : Controller
	{
		private readonly IConfiguration config;
		private readonly ItemService itemService;

		public ItemsController(ItemService itemService, IConfiguration config)
		{
			this.itemService = itemService;
			this.config = config;
		}

		[HttpGet]
		public async Task<IEnumerable<Item>> GetAll()
		{
			var models = await this.itemService.GetAllItemsAsync();

			var host = this.HttpContext.Request.Scheme + "://" + this.HttpContext.Request.Host;

			return models.Select(item => item.ToWebModel(host + this.config["PathTo:ItemPhotos"])).ToList();
		}

		[HttpGet("{id}")]
		public async Task<Item> GetById(Guid id)
		{
			var model = await this.itemService.GetItemByIdAsync(id);

			var host = this.HttpContext.Request.Scheme + "://" + this.HttpContext.Request.Host;

			return model.ToWebModel(host + this.config["PathTo:ItemPhotos"]);
		}

		//[OptimisticConcurrencyFilter]
		//[HttpPut]
		//public async Task<Item> Put(int id)
		//{
		//	//var model = await this.itemService.GetItemByIdAsync(id);

		//	//var host = this.HttpContext.Request.Scheme + "://" + this.HttpContext.Request.Host;

		//	//return model.ToWebModel(host + this.config["PathTo:ItemPhotos"]);
		//}
	}
}