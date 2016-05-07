using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QueryModel = OxHack.Inventory.Query.Models.Item;
using WebModel = OxHack.Inventory.Web.Models.Item;

namespace OxHack.Inventory.Web.Extensions
{
    internal static class ModelExtensions
    {
        public static WebModel ToWebModel(this QueryModel @this, string photoPath, EncryptionService encryptionService)
        {
            byte[] iv;
            var encryptedBytes = encryptionService.EncryptAscii(@this.ConcurrencyId.ToString(), out iv);
            var encryptedConcurrencyId = Convert.ToBase64String(encryptedBytes) + ";" + Convert.ToBase64String(iv);

            return new WebModel(
                @this.Id,
                @this.AdditionalInformation,
                @this.Appearance,
                @this.AssignedLocation,
                @this.Category,
                @this.CurrentLocation,
                @this.IsLoan,
                @this.Manufacturer,
                @this.Model,
                @this.Name,
                @this.Origin,
                @this.Quantity,
                @this.Spec,
				@this.Photos?.ToUris(photoPath),
                encryptedConcurrencyId);
        }

        public static IEnumerable<Uri> ToUris(this IEnumerable<string> @this, string path)
        {
            return @this?.Select(item => new Uri(path + item)).ToList();
        }
    }
}
