using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
	public class ItemCreated : IAggregateEvent
	{
		public ItemCreated(
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
			   List<string> photos
			   )
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

		public int ConcurrencyId
			=> 1;

		public List<string> Photos
		{
			get;
		}

		public dynamic Apply(dynamic aggregate)
		{
			aggregate.AggregateRootId = this.Id;
			aggregate.ConcurrencyId = this.ConcurrencyId;
			aggregate.AdditionalInformation = this.AdditionalInformation;
			aggregate.Appearance = this.Appearance;
			aggregate.AssignedLocation = this.AssignedLocation;
			aggregate.Category = this.Category;
			aggregate.CurrentLocation = this.CurrentLocation;
			aggregate.IsLoan = this.IsLoan;
			aggregate.Manufacturer = this.Manufacturer;
			aggregate.Model = this.Model;
			aggregate.Name = this.Name;
			aggregate.Origin = this.Origin;
			aggregate.Quantity = this.Quantity;
			aggregate.Spec = this.Spec;
			aggregate.Photos = this.Photos.ToList();

			return aggregate;
		}
	}
}
