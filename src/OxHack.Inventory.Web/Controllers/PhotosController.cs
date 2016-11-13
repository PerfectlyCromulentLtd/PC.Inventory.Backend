using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OxHack.Inventory.Cqrs.Commands.Item;
using OxHack.Inventory.Services;
using OxHack.Inventory.Web.Extensions;
using OxHack.Inventory.Web.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Controllers
{
	[Route("api/v1/items/{itemId}/[controller]")]
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
		public async Task<IEnumerable<Uri>> GetItemPhotos(Guid itemId)
		{
			var model = await this.itemService.GetItemByIdAsync(itemId);

			return model.Photos?.ToUris(this.Host + this.PathToPhotos);
		}

		[HttpPost]
		public async Task<Uri> UploadPhoto(Guid itemId, [FromQuery] string concurrencyId)
		{
			var photoData = new byte[(int)this.Request.ContentLength];
			this.Request.Body.Read(photoData, 0, (int)this.Request.ContentLength);

			var folder = Path.Combine(this.hostingEnvironment.WebRootPath, this.PathToPhotos.Trim('/'));

            int decryptedConcurrencyId = concurrencyId.AsDecryptedConcurrencyId(this.encryptionService);

            var command = new AddPhotoCommand(itemId, decryptedConcurrencyId, photoData, folder);
            await this.itemService.IssueCommandAsync(command);

            var result = new Uri(this.Host + this.PathToPhotos + command.FileName);

			return result;
		}

		private string Host
			=> this.HttpContext.Request.Scheme + "://" + this.HttpContext.Request.Host;

		private string PathToPhotos
			=> this.config[this.hostingEnvironment.EnvironmentName + ":ItemPhotos"];
	}
}