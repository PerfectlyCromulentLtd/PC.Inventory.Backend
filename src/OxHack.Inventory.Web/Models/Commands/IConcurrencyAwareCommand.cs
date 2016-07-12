namespace OxHack.Inventory.Web.Models.Commands
{
	public interface IConcurrencyAwareCommand : ICommand
	{
		string ConcurrencyId
		{
			get;
		}
	}
}
