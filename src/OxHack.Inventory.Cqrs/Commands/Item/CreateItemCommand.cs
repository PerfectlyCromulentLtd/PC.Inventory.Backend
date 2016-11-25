using OxHack.Inventory.Cqrs.Events.Item;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
	public class CreateItemCommand : ICommand, IMapToEvent<ItemCreated>
	{
		public CreateItemCommand(
			Guid aggregateRootId,
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
			dynamic issuerMetadata)
		{
			this.Id = aggregateRootId;
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

		public dynamic IssuerMetadata
		{
			get;
		}

		public ItemCreated GetEvent()
		{
			return new ItemCreated(
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
				this.Photos);
		}
	}
}
