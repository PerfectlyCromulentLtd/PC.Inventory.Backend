using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxHack.Inventory.Cqrs.Commands
{
	public interface ICommand : IMessage
	{
		dynamic IssuerMetadata
		{
			get;
		}
	}
}
