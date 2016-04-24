using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs
{
    public interface IMessage
    {
        Guid AggregateRootId
        {
            get;
        }
    }
}
