using OxHack.Inventory.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Extensions
{
	internal static class ModelExtensions
	{
		public static Models.Item ToWebModel(this Query.Models.Item @this, string photoPath, EncryptionService encryptionService)
		{
            byte[] iv;
            var encryptedBytes = encryptionService.Encrypt(@this.ConcurrencyId.ToString(), out iv);
            var encryptedConcurrencyId = Convert.ToBase64String(encryptedBytes) + ";" + Convert.ToBase64String(iv);

            return new Models.Item(
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
                encryptedConcurrencyId
			);
		}

        public static IEnumerable<Uri> ToUris(this IEnumerable<string> @this, string path)
        {
            return @this?.Select(item => new Uri(path + item)).ToList();
        }
	}
}
