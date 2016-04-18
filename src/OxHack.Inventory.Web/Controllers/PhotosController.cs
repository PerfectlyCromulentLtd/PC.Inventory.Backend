﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OxHack.Inventory.Services;
using OxHack.Inventory.Web.Models;
using OxHack.Inventory.Web.Extensions;
using Microsoft.Extensions.Configuration;

namespace OxHack.Inventory.Web.Controllers
{
	[Route("api/items/{itemId}/[controller]")]
	public class PhotosController : Controller
	{
		private readonly IConfiguration config;
		private readonly ItemService itemService;

		public PhotosController(ItemService itemService, IConfiguration config)
		{
			this.itemService = itemService;
			this.config = config;
		}

		[HttpGet]
		public async Task<IEnumerable<Uri>> GetAll(Guid itemId)
		{
			var model = await this.itemService.GetItemByIdAsync(itemId);

			var host = this.HttpContext.Request.Scheme + "://" + this.HttpContext.Request.Host;

            return model.Photos?.ToUris(host + this.config["PathTo:ItemPhotos"]);
		}
	}
}