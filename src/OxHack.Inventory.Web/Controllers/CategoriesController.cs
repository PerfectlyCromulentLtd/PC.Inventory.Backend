using Microsoft.AspNetCore.Mvc;
using OxHack.Inventory.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Controllers
{
	[Route("api/v1/[controller]")]
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