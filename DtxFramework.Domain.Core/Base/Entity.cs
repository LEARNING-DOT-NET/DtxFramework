namespace DtxFramework.Domain.Core.Base
{
	public abstract class Entity : object
	{
		protected Entity() : base()
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
