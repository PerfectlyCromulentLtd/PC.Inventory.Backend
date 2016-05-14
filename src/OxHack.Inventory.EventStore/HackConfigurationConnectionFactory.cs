using Microsoft.Data.Sqlite;
using NEventStore.Persistence.Sql;
using System.Configuration;
using System.Data.Common;

namespace OxHack.Inventory.EventStore
{
    public class HackConfigurationConnectionFactory : ConfigurationConnectionFactory
	{
        private readonly string providerName;
        private readonly string connectionString;

        public HackConfigurationConnectionFactory(string providerName, string connectionString) : base(providerName)
		{
            this.providerName = providerName;
            this.connectionString = connectionString;
		}

		protected override ConnectionStringSettings GetConnectionStringSettings(string connectionName)
		{
			var result = new ConnectionStringSettings(connectionName, this.connectionString, this.providerName);

			return result;
		}

        protected override DbProviderFactory GetFactory(ConnectionStringSettings setting)
        {
            var factory = SqliteFactory.Instance;

            return factory;
        }
    }
}
