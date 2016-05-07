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
using OxHack.Inventory.Web.Services;
using System.Net;
using OxHack.Inventory.Web.Models.Commands;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Security.Cryptography;

namespace OxHack.Inventory.Web.Controllers
{
    [Route("api/[controller]")]
    public class ItemsController : Controller
    {
        private readonly EncryptionService encryptionService;
        private readonly ItemService itemService;
        private readonly IConfiguration config;
        private readonly ReadOnlyDictionary<string, Type> supportedDomainModelTypesByStringName;

        public ItemsController(ItemService itemService, EncryptionService encryptionService, IConfiguration config)
        {
            this.itemService = itemService;
            this.encryptionService = encryptionService;
            this.config = config;

            var supportedDomainModelTypesByStringName = new Dictionary<string, Type>();
            supportedDomainModelTypesByStringName.Add(nameof(CreateItemCommand), typeof(CreateItemCommand));
            supportedDomainModelTypesByStringName.Add(nameof(ChangeAdditionalInformationCommand), typeof(ChangeAdditionalInformationCommand));
            supportedDomainModelTypesByStringName.Add(nameof(ChangeAppearanceCommand), typeof(ChangeAppearanceCommand));
            supportedDomainModelTypesByStringName.Add(nameof(ChangeAssignedLocationCommand), typeof(ChangeAssignedLocationCommand));
            supportedDomainModelTypesByStringName.Add(nameof(ChangeCategoryCommand), typeof(ChangeCategoryCommand));
            supportedDomainModelTypesByStringName.Add(nameof(ChangeCurrentLocationCommand), typeof(ChangeCurrentLocationCommand));
            supportedDomainModelTypesByStringName.Add(nameof(ChangeIsLoanCommand), typeof(ChangeIsLoanCommand));
            supportedDomainModelTypesByStringName.Add(nameof(ChangeManufacturerCommand), typeof(ChangeManufacturerCommand));
            supportedDomainModelTypesByStringName.Add(nameof(ChangeModelCommand), typeof(ChangeModelCommand));
            supportedDomainModelTypesByStringName.Add(nameof(ChangeNameCommand), typeof(ChangeNameCommand));
            supportedDomainModelTypesByStringName.Add(nameof(ChangeOriginCommand), typeof(ChangeOriginCommand));
            supportedDomainModelTypesByStringName.Add(nameof(ChangeQuantityCommand), typeof(ChangeQuantityCommand));
            supportedDomainModelTypesByStringName.Add(nameof(ChangeSpecCommand), typeof(ChangeSpecCommand));

            this.supportedDomainModelTypesByStringName = new ReadOnlyDictionary<string, Type>(supportedDomainModelTypesByStringName);
        }

        [HttpGet]
		public async Task<IEnumerable<Item>> GetAll([FromQuery] string category = null)
        {
			bool byCategory = category != null;

			IEnumerable<Query.Models.Item> models;
			if (byCategory)
			{
				models = await this.itemService.GetItemsByCategoryAsync(category);
			}
			else
			{
				models = await this.itemService.GetAllItemsAsync();
			}
			
            return models.Select(item => item.ToWebModel(this.Host + this.config["PathTo:ItemPhotos"], this.encryptionService)).ToList();
        }

		[HttpGet("{id}")]
        public async Task<Item> GetById(Guid id)
        {
            var model = await this.itemService.GetItemByIdAsync(id);

            return model.ToWebModel(this.Host + this.config["PathTo:ItemPhotos"], this.encryptionService);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateItemCommand command)
        {
            IActionResult result;
            if (!this.ValidateDomainModel(nameof(CreateItemCommand), out result))
            {
                return await Task.FromResult(result);
            }

            var forget = this.itemService.IssueCommandAsync(command.ToDomainCommand());

            return new HttpStatusCodeResult((int)HttpStatusCode.Accepted);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] JObject body)
        {
            IActionResult result;
            IConcurrencyAwareCommand command;
            if (!this.ValidatePut(id, body, out command, out result))
            {
                return await Task.FromResult(result);
            }

            try
            {
                var forget = this.itemService.IssueCommandAsync(command.ToDomainCommand(this.encryptionService));
            }
            catch (CryptographicException e)
            {
                return this.HttpBadRequest("Unable to decrypt ConcurrencyId.  This may be a sign your data is stale.");
            }

            return new HttpStatusCodeResult((int)HttpStatusCode.Accepted);
        }

        private bool ValidatePut(
            Guid resourceId,
            JObject body,
            out IConcurrencyAwareCommand command,
            out IActionResult errorResult)
        {
            bool stillValid = true;
            command = null;
            errorResult = null;

            Type commandType;
            stillValid = this.ValidateDomainModel(out commandType, out errorResult);

            if (stillValid)
            {
                command = body.ToObject(commandType) as IConcurrencyAwareCommand;

                if (command == null)
                {
                    stillValid = false;
                    errorResult = this.HttpBadRequest("Could not deserialize request body to Domain Model type.  Are you sure it's in the correct format?");
                }
            }

            if (stillValid)
            {
                stillValid = this.ValidateCommandId(resourceId, command, out errorResult);
            }

            return stillValid;
        }

        private bool ValidateDomainModel(string expectedDomainModel, out IActionResult errorResult)
        {
            bool stillValid = true;
            errorResult = null;

            string actualDomainModel;
            stillValid = this.ExtractDomainModelFromContentType(out actualDomainModel, out errorResult);

            if (stillValid && actualDomainModel != expectedDomainModel)
            {
                stillValid = false;
                errorResult = this.HttpBadRequest($"Content-Type 'domain-model' value does not match expected Domain Model of '{expectedDomainModel}'");
            }

            return stillValid;
        }

        private bool ValidateDomainModel(out Type commandType, out IActionResult errorResult)
        {
            bool stillValid = true;
            errorResult = null;

            string typeName;
            stillValid = this.ExtractDomainModelFromContentType(out typeName, out errorResult);

            if (stillValid && !this.supportedDomainModelTypesByStringName.ContainsKey(typeName))
            {
                stillValid = false;
                errorResult = this.HttpBadRequest("Content-Type 'domain-model' value does not match supported Domain Models.");
            }

            commandType = this.supportedDomainModelTypesByStringName[typeName];

            return stillValid;
        }

        private bool ExtractDomainModelFromContentType(out string domainModel, out IActionResult errorResult)
        {
            bool stillValid = true;
            domainModel = null;
            errorResult = null;

            var domainModelTokens =
                this.HttpContext.Request.ContentType?
                    .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .FirstOrDefault(item => item.ToLowerInvariant().StartsWith("domain-model="))
                        ?.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

            if (domainModelTokens == null || domainModelTokens.Length != 2)
            {
                stillValid = false;
                errorResult = this.HttpBadRequest("Could not parse 'domain-model' value from Content-Type header");
            }
            else
            {
                domainModel = domainModelTokens[1];
            }

            return stillValid;
        }

        private bool ValidateCommandId(Guid resourceId, IConcurrencyAwareCommand command, out IActionResult errorResult)
        {
            bool stillValid = true;
            errorResult = null;

            if (resourceId != command.Id)
            {
                stillValid = false;
                errorResult = HttpBadRequest("Resource Id and command Id do not match.");
            }

            return stillValid;
        }

        private string Host
        {
            get
            {
                return this.HttpContext.Request.Scheme + "://" + this.HttpContext.Request.Host;
            }
        }
    }
}