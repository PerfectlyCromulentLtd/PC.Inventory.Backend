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
	[Route("api/v1/")]
	public class PhotosController : ControllerBase
	{
		private readonly ItemService itemService;

		public PhotosController(ItemService itemService, EncryptionService encryptionService, IHostingEnvironment hostingEnvironment, IConfiguration config)
			: base(encryptionService, hostingEnvironment, config)
		{
			this.itemService = itemService;
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
		public async Task<IActionResult> UploadAndAddPhoto(
			Guid itemId,
			[FromHeader] string concurrencyId)
		{
			IActionResult error;
			var clientMetadata = this.ExtractClientMetadata(out error);
			if (error != null)
			{
				return error;
			}

			byte[] photoData = await this.ReadRequestBodyToBufferAsync();

			var folder = Path.Combine(this.HostingEnvironment.WebRootPath, this.PathToPhotos.Trim('/'));

			int decryptedConcurrencyId = this.GetDecryptedConcurrencyId(concurrencyId);

			var command = new UploadAndAddPhotoCommand(itemId, decryptedConcurrencyId, photoData, folder, clientMetadata);
			await this.itemService.IssueCommandAsync(command);

			var result = new Uri(this.Host + this.PathToPhotos + command.ResultingFileName);

			return this.Ok(result);
		}

		[HttpPost]
		[Route("[controller]")]
		public async Task<IActionResult> UploadPhoto()
		{
			IActionResult error;
			var clientMetadata = this.ExtractClientMetadata(out error);
			if (error != null)
			{
				return error;
			}

			byte[] photoData = await this.ReadRequestBodyToBufferAsync();

			var folder = Path.Combine(this.HostingEnvironment.WebRootPath, this.PathToPhotos.Trim('/'));

			var command = new UploadPhotoCommand(photoData, folder, clientMetadata);
			await this.itemService.IssueCommandAsync(command);

			var result = new Uri(this.Host + this.PathToPhotos + command.ResultingFileName);

			return this.Ok(result);
		}

		[HttpDelete]
		[Route("items/{itemId}/[controller]/{photo}")]
		public async Task<IActionResult> UnlinkPhoto(
			Guid itemId,
			string photo,
			[FromHeader] string concurrencyId)
		{
			IActionResult error;
			var clientMetadata = this.ExtractClientMetadata(out error);
			if (error != null)
			{
				return error;
			}

			int decryptedConcurrencyId = this.GetDecryptedConcurrencyId(concurrencyId);
			var command = new RemovePhotoCommand(itemId, decryptedConcurrencyId, photo, clientMetadata);

			await this.itemService.IssueCommandAsync(command);

			return this.Ok();
		}
	}
}