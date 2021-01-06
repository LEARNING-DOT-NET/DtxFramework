namespace DtxFramework.Domain.Core.Events
{
	public abstract class Event : object
	{
		protected Event() : base()
		{
			Timestamp =
				System.DateTime.Now;
		}

		public System.DateTime Timestamp { get; protected set; }
	}
}
