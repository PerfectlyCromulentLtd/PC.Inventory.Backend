using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events
{
    public interface IConcurrencyAwareEvent : IEvent, IConcurrencyAware
    {
    }
}
