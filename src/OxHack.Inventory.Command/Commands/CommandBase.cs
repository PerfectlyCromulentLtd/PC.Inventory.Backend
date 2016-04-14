using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Command
{
    public abstract class CommandBase
    {
        protected CommandBase(Guid id)
        {
            this.Id = id;
        }

        public Guid Id
        {
            get;
        }
    }

    public abstract class CommandBase<T> : CommandBase
    {
        public CommandBase(Guid id, T payload) 
            : base(id)
        {
            this.Payload = payload;
        }

        public T Payload
        {
            get;
        }
    }
}
