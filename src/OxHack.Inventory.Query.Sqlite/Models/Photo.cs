using System;
using System.Collections.Generic;

namespace OxHack.Inventory.Query.Sqlite.Models
{
    internal partial class Photo
    {
        public string Filename { get; set; }
        public string ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
