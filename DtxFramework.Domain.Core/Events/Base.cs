namespace DtxFramework.Domain.Core.Events
{
	public abstract class Base : object
	{
		public Base() : base()
		{
			TypeName =
				GetType().Name;

			Timestamp =
				System.DateTime.Now;
		}

		public string TypeName { get; protected set; }

		public System.DateTime Timestamp { get; protected set; }
	}
}
