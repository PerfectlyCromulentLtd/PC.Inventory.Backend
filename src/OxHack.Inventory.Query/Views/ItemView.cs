using OxHack.Inventory.Cqrs;
using OxHack.Inventory.Cqrs.Events.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Query.Views
{
    public class ItemView : IHandles<NameChanged>
    {
        public void Handle(NameChanged @event)
        {
            throw new NotImplementedException();
        }
    }
}
