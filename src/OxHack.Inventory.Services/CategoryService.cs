using OxHack.Inventory.Query.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OxHack.Inventory.Query.Models;
using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Commands;

namespace OxHack.Inventory.Services
{
	public class CategoryService
	{
		private readonly ICategoryRepository categoryRepository;

		public CategoryService(ICategoryRepository categoryRepository)
		{
			this.categoryRepository = categoryRepository;
		}

		public async Task<IEnumerable<string>> GetAllCategoriesAsync()
		{
			return await this.categoryRepository.GetAllCategoriesAsync();
		}
	}
}
