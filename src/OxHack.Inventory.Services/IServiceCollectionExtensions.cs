using Microsoft.Extensions.DependencyInjection;

namespace OxHack.Inventory.Services
{
    public static class IServiceCollectionExtensions
    {
        public static void AddDomainServices(this IServiceCollection @this)
        {
            @this.AddTransient<ItemService>();
        }
    }
}
