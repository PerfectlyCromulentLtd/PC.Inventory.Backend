using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web
{
	internal class Constants
	{
		public const string InventoryClientNameHttpHeader = "Inventory-Client-Name";
		public const string InventoryClientVersionHttpHeader = "Inventory-Client-Version";
		public const string InventoryClientIdHttpHeader = "Inventory-Client-UniqueId";
		public const string MissingHttpHeaderMessage = "HTTP Headers " + Constants.InventoryClientNameHttpHeader + ", " + Constants.InventoryClientVersionHttpHeader + ", and " + Constants.InventoryClientIdHttpHeader + " are mandatory.";
	}
}
