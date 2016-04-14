using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Events.Item
{
    public class AdditionalInformationChanged : IEvent
    {
        public Guid Id
        {
            get;
        }

        public string AdditionalInformation
        {
            get;
        }
    }
}
