namespace DtxFramework.Domain.Core.Commands
{
	public abstract class Command : Events.Message
	{
		protected Command() : base()
		{
			Timestamp =
				System.DateTime.Now;
		}

		public System.DateTime Timestamp { get; protected set; }
	}
}
