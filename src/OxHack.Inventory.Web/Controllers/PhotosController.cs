using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OxHack.Inventory.Cqrs.Commands.Item;
using OxHack.Inventory.Cqrs.Commands.Photo;
using OxHack.Inventory.Services;
using OxHack.Inventory.Web.Extensions;
using OxHack.Inventory.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Controllers
{
	[Route("api/v1/")]
	public class PhotosController : Controller
	{
		private readonly ItemService itemService;
		private readonly EncryptionService encryptionService;
		private readonly IConfiguration config;
		private readonly IHostingEnvironment hostingEnvironment;

		public PhotosController(ItemService itemService, EncryptionService encryptionService, IConfiguration config, IHostingEnvironment hostingEnvironment)
		{
			this.itemService = itemService;
			this.encryptionService = encryptionService;
			this.config = config;
			this.hostingEnvironment = hostingEnvironment;
		}

		[HttpGet]
		[Route("items/{itemId}/[controller]")]
		public async Task<IEnumerable<Uri>> GetItemPhotos(Guid itemId)
		{
			var model = await this.itemService.GetItemByIdAsync(itemId);

			return model.Photos?.ToUris(this.Host + this.PathToPhotos);
		}

		[HttpPost]
		[Route("items/{itemId}/[controller]")]
		public async Task<Uri> UploadAndAddPhoto(Guid itemId, [FromHeader] string concurrencyId)
		{
			byte[] photoData = await this.ReadRequestBodyToBufferAsync();

			var folder = Path.Combine(this.hostingEnvironment.WebRootPath, this.PathToPhotos.Trim('/'));

			int decryptedConcurrencyId = this.GetDecryptedConcurrencyId(concurrencyId);

			var command = new UploadAndAddPhotoCommand(itemId, decryptedConcurrencyId, photoData, folder);
			await this.itemService.IssueCommandAsync(command);

			var result = new Uri(this.Host + this.PathToPhotos + command.ResultingFileName);

			return result;
		}

		[HttpPost]
		[Route("[controller]")]
		public async Task<Uri> UploadPhoto()
		{
			byte[] photoData = await this.ReadRequestBodyToBufferAsync();

			var folder = Path.Combine(this.hostingEnvironment.WebRootPath, this.PathToPhotos.Trim('/'));

			var command = new UploadPhotoCommand(photoData, folder);
			await this.itemService.IssueCommandAsync(command);

			var result = new Uri(this.Host + this.PathToPhotos + command.ResultingFileName);

			return result;
		}

		private async Task<byte[]> ReadRequestBodyToBufferAsync()
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

		[HttpDelete]
		[Route("items/{itemId}/[controller]/{photo}")]
		public async Task UnlinkPhoto(Guid itemId, string photo, [FromHeader] string concurrencyId)
		{
			int decryptedConcurrencyId = this.GetDecryptedConcurrencyId(concurrencyId);
			var command = new RemovePhotoCommand(itemId, decryptedConcurrencyId, photo);

			await this.itemService.IssueCommandAsync(command);
		}

		private int GetDecryptedConcurrencyId(string concurrencyId)
		{
			return concurrencyId.AsDecryptedConcurrencyId(this.encryptionService);
		}

		private string Host
			=> this.HttpContext.Request.Scheme + "://" + this.HttpContext.Request.Host;

		private string PathToPhotos
			=> this.config[this.hostingEnvironment.EnvironmentName + ":ItemPhotos"];
	}
}