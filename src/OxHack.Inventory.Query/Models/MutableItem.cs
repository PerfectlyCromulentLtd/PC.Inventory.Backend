using OxHack.Inventory.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Models
{
	public sealed class MutableItem : Item
	{
		internal MutableItem()
		{
		}

		public MutableItem(
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
				: base(
					  id, 
					  additionalInformation, 
					  appearance, 
					  assignedLocation, 
					  category, 
					  currentLocation, 
					  isLoan, 
					  manufacturer, 
					  model, 
					  name, 
					  origin, 
					  quantity, 
					  spec, 
					  photos, 
					  concurrencyId)
		{
		}

		public Guid AggregateRootId
		{
			get
			{
				return base.Id;
			}
			set
			{
				base.Id = value;
			}
		}

		public new string Name
		{
			get
			{
				return base.Name;
			}
			set
			{
				base.Name = value;
			}
		}

		public new string Manufacturer
		{
			get
			{
				return base.Manufacturer;
			}
			set
			{
				base.Manufacturer = value;
			}
		}

		public new string Model
		{
			get
			{
				return base.Model;
			}
			set
			{
				base.Model = value;
			}
		}

		public new int Quantity
		{
			get
			{
				return base.Quantity;
			}
			set
			{
				base.Quantity = value;
			}
		}

		public new string Category
		{
			get
			{
				return base.Category;
			}
			set
			{
				base.Category = value;
			}
		}

		public new string Spec
		{
			get
			{
				return base.Spec;
			}
			set
			{
				base.Spec = value;
			}
		}

		public new string Appearance
		{
			get
			{
				return base.Appearance;
			}
			set
			{
				base.Appearance = value;
			}
		}

		public new string AssignedLocation
		{
			get
			{
				return base.AssignedLocation;
			}
			set
			{
				base.AssignedLocation = value;
			}
		}

		public new string CurrentLocation
		{
			get
			{
				return base.CurrentLocation;
			}
			set
			{
				base.CurrentLocation = value;
			}
		}

		public new bool IsLoan
		{
			get
			{
				return base.IsLoan;
			}
			set
			{
				base.IsLoan = value;
			}
		}

		public new string Origin
		{
			get
			{
				return base.Origin;
			}
			set
			{
				base.Origin = value;
			}
		}

		public new string AdditionalInformation
		{
			get
			{
				return base.AdditionalInformation;
			}
			set
			{
				base.AdditionalInformation = value;
			}
		}

		public new IEnumerable<string> Photos
		{
			get
			{
				return base.Photos;
			}
			set
			{
				base.Photos = value;
			}
		}

		public new int ConcurrencyId
		{
			get
			{
				return base.ConcurrencyId;
			}
			set
			{
				base.ConcurrencyId = value;
			}
		}
	}
}
