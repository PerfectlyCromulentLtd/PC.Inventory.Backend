﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class ManufacturerChanged : IEvent
    {
        public Guid Id
        {
            get;
        }

        public string Manufacturer
        {
            get;
        }
    }
}
