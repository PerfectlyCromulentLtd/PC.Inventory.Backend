using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Web.Extensions
{
	internal static class ModelExtensions
	{
		public static Models.Item ToWebModel(this Query.Models.Item @this, string photoPath)
		{
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
				@this.Photos?.Select(item => new Uri(photoPath + item)).ToList()
			);
		}
	}
}
