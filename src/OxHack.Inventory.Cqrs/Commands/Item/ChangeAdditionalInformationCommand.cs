using OxHack.Inventory.Cqrs.Events.Item;
using System;

namespace OxHack.Inventory.Cqrs.Commands.Item
{
	public class ChangeAdditionalInformationCommand : ICommand, IConcurrencyAware, IMapToEvent<AdditionalInformationChanged>
	{
		public ChangeAdditionalInformationCommand(Guid aggregateRootId, int concurrencyId, string additionalInformation)
		{
			this.Id = aggregateRootId;
			this.ConcurrencyId = concurrencyId;
			this.AdditionalInformation = additionalInformation;
		}

		public Guid Id
		{
			get;
		}

		public int ConcurrencyId
		{
			get;
		}

		public string AdditionalInformation
		{
			get;
		}

		public AdditionalInformationChanged GetEvent()
		{
			return new AdditionalInformationChanged(this.Id, this.ConcurrencyId + 1, this.AdditionalInformation);
		}
	}
}
