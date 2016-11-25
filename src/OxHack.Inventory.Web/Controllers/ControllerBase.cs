using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using OxHack.Inventory.Cqrs.Commands.Item;
using OxHack.Inventory.Cqrs.Commands.Photo;
using OxHack.Inventory.Services;
using OxHack.Inventory.Web.Extensions;
using OxHack.Inventory.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Controllers
{
	public abstract class ControllerBase : Controller
	{
		private readonly IConfiguration config;

		public ControllerBase(EncryptionService encryptionService, IHostingEnvironment hostingEnvironment, IConfiguration config)
		{
			this.EncryptionService = encryptionService;
			this.HostingEnvironment = hostingEnvironment;
			this.config = config;
		}

		protected IEnumerable<KeyValuePair<string, StringValues>> ExtractClientMetadata(out IActionResult error)
		{
			error = null;
			var clientHeaders =
				this.Request.Headers.Where(pair => pair.Key.ToLowerInvariant().StartsWith("inventory-client-")).ToDictionary(key => key.Key, value => value.Value);

			var mandatoryHeaderKeys = new string[] { Constants.InventoryClientIdHttpHeader, Constants.InventoryClientNameHttpHeader, Constants.InventoryClientVersionHttpHeader };

			if (clientHeaders.Keys.Intersect(mandatoryHeaderKeys).Count() != mandatoryHeaderKeys.Length)
			{
				error = this.BadRequest(Constants.MissingHttpHeaderMessage);
			}

			return clientHeaders;
		}

		protected int GetDecryptedConcurrencyId(string concurrencyId)
		{
			return concurrencyId.AsDecryptedConcurrencyId(this.EncryptionService);
		}

		protected async Task<byte[]> ReadRequestBodyToBufferAsync()
		{
			byte[] buffer;
			var stream = this.Request.Body;
			var length = (int)this.Request.ContentLength.Value;

			buffer = new byte[length];
			int bytesRead = 0;
			for (int i = 0; i < 60; i++)
			{
				bytesRead += await this.Request.Body.ReadAsync(buffer, bytesRead, length - bytesRead);
				if (bytesRead >= length)
				{
					break;
				}
				else
				{
					// Mega Hack: We've read all the data available (so far) but haven't hit the end of the stream.  Wait a sec.
					await Task.Delay(1000);
				}
			}

			return buffer;
		}

		protected IHostingEnvironment HostingEnvironment
		{
			get;
		}

		protected EncryptionService EncryptionService
		{
			get;
		}

		protected string Host
			=> this.HttpContext.Request.Scheme + "://" + this.HttpContext.Request.Host;

		protected string PathToPhotos
			=> this.config[this.HostingEnvironment.EnvironmentName + ":ItemPhotos"];
	}
}