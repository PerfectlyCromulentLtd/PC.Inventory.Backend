using System;
using System.Collections.Generic;

namespace OxHack.Inventory.Data.Sqlite.Models
{
    internal partial class Item
    {
        public Item()
        {
            this.Photos = new HashSet<Photo>();
        }

        public long Id { get; set; }
        public string AdditionalInformation { get; set; }
        public string Appearance { get; set; }
        public string AssignedLocation { get; set; }
        public string Category { get; set; }
        public string CurrentLocation { get; set; }
        public long IsLoan { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Name { get; set; }
        public string Origin { get; set; }
        public long Quantity { get; set; }
        public string Spec { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }
    }
}
