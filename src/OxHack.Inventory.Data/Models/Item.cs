using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OxHack.Inventory.Data.Models
{
	public class Item
	{
		public int Id
		{
			get;
			internal set;
		}

		public String Name
		{
			get;
			internal set;
		}

		public String Manufacturer
		{
			get;
			internal set;
		}

		public String Model
		{
			get;
			internal set;
		}

		public int Quantity
		{
			get;
			internal set;
		}

		public String Category
		{
			get;
			internal set;
		}

		public String Spec
		{
			get;
			internal set;
		}

		public String Appearance
		{
			get;
			internal set;
		}

		public String AssignedLocation
		{
			get;
			internal set;
		}

		public String CurrentLocation
		{
			get;
			internal set;
		}

		public bool IsLoan
		{
			get;
			internal set;
		}

		public String Origin
		{
			get;
			internal set;
		}

		public String AdditionalInformation
		{
			get;
			internal set;
		}

		public String Photos
		{
			get;
			internal set;
		}

		//public IEnumerable<String> Photos
		//{
		//	get;
		//	internal set;
		//}

		//public bool HasPhoto
		//{
		//	get
		//	{
		//		return this.Photos?.Any(item => !String.IsNullOrWhiteSpace(item)) ?? false;
		//	}
		//}
	}
}
