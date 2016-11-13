using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OxHack.Inventory.Services;
using OxHack.Inventory.Web.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Controllers
{
	[Route("api/v1/items/{itemId}/[controller]")]
	public class PhotosController : Controller
	{
		private readonly IConfiguration config;
		private readonly ItemService itemService;
		private readonly IHostingEnvironment hostingEnvironment;

		public PhotosController(ItemService itemService, IConfiguration config, IHostingEnvironment hostingEnvironment)
		{
			this.itemService = itemService;
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
		public async Task<Uri> UploadPhoto(Guid itemId)
		{
			var photoData = new byte[(int)this.Request.ContentLength];
			this.Request.Body.Read(photoData, 0, (int)this.Request.ContentLength);

			var folder = Path.Combine(this.hostingEnvironment.WebRootPath, this.PathToPhotos.Trim('/'));

			var filename = await this.itemService.AddPhotoToItemAsync(itemId, photoData, folder);

			var result = new Uri(this.Host + this.PathToPhotos + filename);

			return result;
		}

		private string Host
			=> this.HttpContext.Request.Scheme + "://" + this.HttpContext.Request.Host;

		private string PathToPhotos
			=> this.config[this.hostingEnvironment.EnvironmentName + ":ItemPhotos"];
	}
}