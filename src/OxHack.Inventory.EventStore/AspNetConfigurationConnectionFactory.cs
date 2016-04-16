using Microsoft.Extensions.Configuration;
using NEventStore.Persistence.Sql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.EventStore
{
	public class AspNetConfigurationConnectionFactory : ConfigurationConnectionFactory
	{
		private readonly IConfigurationRoot config;
		private readonly string connectionString;
		private readonly string providerName;

		public AspNetConfigurationConnectionFactory(string configurationkey, string providerName, IConfigurationRoot configuration) : base("unimportant")
		{
			this.config = configuration;
			this.connectionString = configuration[configurationkey];
			this.providerName = providerName;
		}

		protected override ConnectionStringSettings GetConnectionStringSettings(string connectionName)
		{
			var result = new ConnectionStringSettings("unimportant", this.connectionString, this.providerName);

			return result;
		}
	}
}
