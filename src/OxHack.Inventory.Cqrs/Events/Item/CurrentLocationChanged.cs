using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class CurrentLocationChanged : IEvent
    {
        public Guid Id
        {
            get;
        }

        public string CurrentLocation
        {
            get;
        }
    }
}
