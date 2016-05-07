using OxHack.Inventory.Cqrs.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs
{
    public interface IHandle<in TMessage> where TMessage : IMessage
    {
		Task Handle(TMessage message);
    }
}
