using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OxHack.Inventory.Data.Repositories;
using OxHack.Inventory.Data.Sqlite.Repositories;

namespace OxHack.Inventory.Data.Sqlite
{
	public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
			var connection = configuration["Production:SqliteConnectionString"];

			services
				.AddEntityFramework()
				.AddSqlite()
				.AddDbContext<InventoryDbContext>(options => options.UseSqlite(connection));


			services.AddTransient<IItemRepository, ItemRepository>();
		}
    }
}
