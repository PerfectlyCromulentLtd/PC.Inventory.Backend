using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class QuantityChanged : IEvent
    {
        public Guid Id
        {
            get;
        }

        public int Quantity
        {
            get;
        }
    }
}
