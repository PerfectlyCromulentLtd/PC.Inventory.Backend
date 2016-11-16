using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Photo
{
	public class PhotoUploaded : IEvent
	{
		public PhotoUploaded(string filename)
		{
			this.FileName = filename;
		}

		public string FileName
		{
			get;
		}
	}
}
