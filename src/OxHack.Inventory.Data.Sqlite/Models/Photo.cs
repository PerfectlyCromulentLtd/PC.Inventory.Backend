using System;
using System.Collections.Generic;

namespace OxHack.Inventory.Data.Sqlite.Models
{
    internal partial class Photo
    {
        public string Filename { get; set; }
        public long ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
