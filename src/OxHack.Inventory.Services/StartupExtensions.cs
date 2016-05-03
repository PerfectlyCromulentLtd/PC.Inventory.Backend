using Microsoft.Extensions.DependencyInjection;

namespace OxHack.Inventory.Services
{
    public static class StartupExtensions
    {
        public static void AddDomainServices(this IServiceCollection @this)
        {
            @this.AddTransient<ItemService>();
			@this.AddTransient<CategoryService>();
		}
    }
}
