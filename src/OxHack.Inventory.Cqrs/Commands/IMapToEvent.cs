using OxHack.Inventory.Cqrs.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Commands
{
    public interface IMapToEvent<TEvent> where TEvent : IEvent
    {
        TEvent GetEvent();
    }
}
