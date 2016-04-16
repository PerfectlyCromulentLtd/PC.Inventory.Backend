using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OxHack.Inventory.Query.Repositories;
using OxHack.Inventory.Query.Sqlite.Repositories;

namespace OxHack.Inventory.Query.Sqlite
{
	public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
			var connection = configuration["Production:SqliteReadModelConnectionString"];

			services
				.AddEntityFramework()
				.AddSqlite()
				.AddDbContext<InventoryDbContext>(options => options.UseSqlite(connection));


			services.AddTransient<IItemRepository, ItemRepository>();
		}
    }
}
