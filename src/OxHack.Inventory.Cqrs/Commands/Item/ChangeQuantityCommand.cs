﻿using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
    public class ChangeQuantityCommand : ICommand, IConcurrencyAware, IMapToEvent<QuantityChanged>
    {
        public ChangeQuantityCommand(Guid aggregateRootId, int concurrencyId, int quantity)
        {
            this.Id = aggregateRootId;
            this.ConcurrencyId = concurrencyId;
            this.Quantity = quantity;
        }

        public Guid Id
        {
            get;
        }

        public int ConcurrencyId
        {
            get;
        }

        public int Quantity
        {
            get;
        }

        public QuantityChanged GetEvent()
        {
            return new QuantityChanged(this.Id, this.ConcurrencyId + 1, this.Quantity);
        }
    }
}
