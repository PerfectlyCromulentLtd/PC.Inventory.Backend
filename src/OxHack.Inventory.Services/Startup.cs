using Microsoft.Extensions.DependencyInjection;

namespace OxHack.Inventory.Services
{
	public static class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
			services.AddTransient<ItemService, ItemService>();
		}
    }
}
