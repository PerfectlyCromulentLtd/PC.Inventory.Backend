using OxHack.Inventory.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Models
{
    public abstract class Item : IConcurrencyAware
    {
		protected Item()
		{
		}

        protected Item(
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
            IEnumerable<string> photos,
            int concurrencyId)
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
            this.Photos = photos ?? Enumerable.Empty<string>();
            this.ConcurrencyId = concurrencyId;
        }

		public Guid Id
		{
			get;
			internal set;
		}

		public string Name
		{
			get;
			internal set;
		}

		public string Manufacturer
		{
			get;
			internal set;
		}

		public string Model
		{
			get;
			internal set;
		}

		public int Quantity
		{
			get;
			internal set;
		}

		public string Category
		{
			get;
			internal set;
		}

		public string Spec
		{
			get;
			internal set;
		}

		public string Appearance
		{
			get;
			internal set;
		}

		public string AssignedLocation
		{
			get;
			internal set;
		}

		public string CurrentLocation
		{
			get;
			internal set;
		}

		public bool IsLoan
		{
			get;
			internal set;
		}

		public string Origin
		{
			get;
			internal set;
		}

		public string AdditionalInformation
		{
			get;
			internal set;
		}

		public IEnumerable<string> Photos
		{
			get;
			internal set;
        }

        public int ConcurrencyId
        {
            get;
            internal set;
        }
    }
}
