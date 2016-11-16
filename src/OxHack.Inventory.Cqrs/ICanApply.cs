using OxHack.Inventory.Cqrs.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs
{
    public interface ICanApply<in TMessage> where TMessage : IConcurrencyAwareEvent
    {
		void Apply(TMessage message);
    }
}
