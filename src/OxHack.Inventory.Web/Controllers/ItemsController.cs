using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OxHack.Inventory.Services;
using OxHack.Inventory.Web.Models;
using OxHack.Inventory.Web.Extensions;
using Microsoft.Extensions.Configuration;
using OxHack.Inventory.Web.Models.Commands.Item;
using OxHack.Inventory.Cqrs;
using System.Security.Cryptography;
using System.Text;
using OxHack.Inventory.Web.Services;

namespace OxHack.Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly EncryptionService encryptionService;
        private readonly ItemService itemService;
        private readonly IConfiguration config;

        public ItemsController(ItemService itemService, EncryptionService encryptionService, IConfiguration config)
        {
            this.itemService = itemService;
            this.encryptionService = encryptionService;
            this.config = config;
        }

        [HttpGet]
        public async Task<IEnumerable<Item>> GetAll()
        {
            var models = await this.itemService.GetAllItemsAsync();

            return models.Select(item => item.ToWebModel(this.Host + this.config["PathTo:ItemPhotos"], this.encryptionService)).ToList();
        }

        [HttpGet("{id}")]
        public async Task<Item> GetById(Guid id)
        {
            var model = await this.itemService.GetItemByIdAsync(id);

            return model.ToWebModel(this.Host + this.config["PathTo:ItemPhotos"], this.encryptionService);
        }

        //[OptimisticConcurrencyFilter]
        [HttpPost]
        //public async Task<Item> Post()
        public async Task<IActionResult> Post([FromBody] CreateItemCommand command)
        {
            IActionResult result;
            if (!this.ContainsCorrectDomainModel(nameof(CreateItemCommand), out result))
            {
                return await Task.FromResult(result);
            }

            return await Task.FromResult(new ObjectResult(command));
        }

        private bool ContainsCorrectDomainModel(string expectedDomainModel, out IActionResult errorResult)
        {
            bool result = true;
            errorResult = null;

            var domainModelTokens =
                            this.HttpContext.Request.ContentType?
                                .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                                    .FirstOrDefault(item => item.ToLowerInvariant().StartsWith("domain-model="))
                                    ?.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

            string domainModel = null;
            if (domainModelTokens.Length == 2)
            {
                domainModel = domainModelTokens[1];
            }

            if (domainModel != expectedDomainModel)
            {
                result = false;
                errorResult = HttpBadRequest("Expected to find 'domain-model=" + expectedDomainModel + "' in Content-Type");
            }

            return result;
        }

        private string Host
        {
            get
            {
                return this.HttpContext.Request.Scheme + "://" + this.HttpContext.Request.Host; ;
            }
        }
    }
}