using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events
{
    public interface IEvent
    {
        Guid Id
        {
            get;
        }
    }
}
