using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NEventStore;
using NEventStore.Dispatcher;
using NEventStore.Persistence.Sql.SqlDialects;
using System.Linq;
using OxHack.Inventory.Cqrs;
using System;

namespace OxHack.Inventory.EventStore
{
	public static class StartupExtensions
	{
		[Obsolete("Uses NEventStore's (apparently) deprecated Dispatcher Model.  Will need to change.")]
		public static void AddEventStore(this IServiceCollection @this, IConfigurationRoot configuration)
		{
            var providerName = "Microsoft.Data.SQLite";
            var connectionString = configuration["Production:SqliteEventStoreConnectionString"];

            var configFactory = new HackConfigurationConnectionFactory(providerName, connectionString);

			var meh = configFactory.GetDbProviderFactoryType();

			var provider = @this.BuildServiceProvider();
			var bus = provider.GetService<IBus>();

			var store = 
				Wireup.Init()
					.UsingSynchronousDispatchScheduler(new CommitDispatcher(bus))
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
