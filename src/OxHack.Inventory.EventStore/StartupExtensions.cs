using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NEventStore;
using NEventStore.Persistence.Sql.SqlDialects;
using OxHack.Inventory.Cqrs;

namespace OxHack.Inventory.EventStore
{
	public static class StartupExtensions
	{
		public static void AddEventStore(this IServiceCollection @this, IConfigurationRoot configuration)
		{
            var providerName = "Microsoft.Data.SQLite";
            var connectionString = configuration["Production:SqliteEventStoreConnectionString"];

            var configFactory = new HackConfigurationConnectionFactory(providerName, connectionString);

			var meh = configFactory.GetDbProviderFactoryType();

			var store = 
				Wireup.Init()
					.UsingSqlPersistence(configFactory)
						.WithDialect(new SqliteDialect())
						.InitializeStorageEngine()
						.UsingJsonSerialization()
					.Build();

            var eventStore = new NEventStoreEventStore(store);

            @this.AddSingleton<IEventStore, NEventStoreEventStore>(s => eventStore);
		}
    }
}
