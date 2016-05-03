using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OxHack.Inventory.Services;
using OxHack.Inventory.Web.Models;
using OxHack.Inventory.Web.Extensions;
using Microsoft.Extensions.Configuration;
using OxHack.Inventory.Web.Models.Commands.Item;
using OxHack.Inventory.Web.Services;
using System.Net;
using OxHack.Inventory.Web.Models.Commands;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using Microsoft.AspNet.Hosting;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events;

namespace OxHack.Inventory.Web.Controllers
{
	[Route("api/[controller]")]
	public class CategoriesController : Controller
	{
		private readonly CategoryService categoryService;

		public CategoriesController(CategoryService categoryService)
		{
			this.categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IEnumerable<string>> GetAll()
		{
			return await this.categoryService.GetAllCategoriesAsync();
		}
	}
}