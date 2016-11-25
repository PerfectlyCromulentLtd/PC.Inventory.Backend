using OxHack.Inventory.Cqrs.Events.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    [Obsolete("Get rid of this class ASAP.  It was just created to help speed up development.  You should be sending updates through Deltas over PATCH.")]
    public class UpdateItemCommand : ICommand, IConcurrencyAware, IMapToEvent<ItemUpdated>
    {
        public UpdateItemCommand(
           Guid id,
           string additionalInformation,
           string appearance,
           string assignedLocation,
           string category,
           string currentLocation,
           bool isLoan,
           string manufacturer,
           string model,
           string name,
           string origin,
           int quantity,
           string spec,
           List<string> photos,
		   int concurrencyId,
		   dynamic issuerMetadata)
        {
            this.Id = id;
            this.AdditionalInformation = additionalInformation;
            this.Appearance = appearance;
            this.AssignedLocation = assignedLocation;
            this.Category = category;
            this.CurrentLocation = currentLocation;
            this.IsLoan = isLoan;
            this.Manufacturer = manufacturer;
            this.Model = model;
            this.Name = name;
            this.Origin = origin;
            this.Quantity = quantity;
            this.Spec = spec;
            this.Photos = photos;
			this.ConcurrencyId = concurrencyId;
			this.IssuerMetadata = issuerMetadata;
		}

        public Guid Id
        {
            get;
        }

        public string Name
        {
            get;
        }

        public string Manufacturer
        {
            get;
        }

        public string Model
        {
            get;
        }

        public int Quantity
        {
            get;
        }

        public string Category
        {
            get;
        }

        public string Spec
        {
            get;
        }

        public string Appearance
        {
            get;
        }

        public string AssignedLocation
        {
            get;
        }

        public string CurrentLocation
        {
            get;
        }

        public bool IsLoan
        {
            get;
        }

        public string Origin
        {
            get;
        }

        public string AdditionalInformation
        {
            get;
        }

        public List<string> Photos
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
		}

		public dynamic IssuerMetadata
		{
			get;
		}

		public ItemUpdated GetEvent()
        {
            return new ItemUpdated(
                this.Id,
                this.AdditionalInformation,
                this.Appearance,
                this.AssignedLocation,
                this.Category,
                this.CurrentLocation,
                this.IsLoan,
                this.Manufacturer,
                this.Model,
                this.Name,
                this.Origin,
                this.Quantity,
                this.Spec,
                this.Photos,
                this.ConcurrencyId + 1);
        }
    }
}
