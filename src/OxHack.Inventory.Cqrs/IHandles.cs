using OxHack.Inventory.Cqrs.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs
{
    public interface IHandles<TEvent> where TEvent : IEvent
    {
		void Handle(TEvent @event);
    }
}
