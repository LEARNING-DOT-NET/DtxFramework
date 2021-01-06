namespace DtxFramework.Domain.Core.Commands
{
	public abstract class Command : Events.Message
	{
		protected Command() : base()
		{
		}
	}
}
