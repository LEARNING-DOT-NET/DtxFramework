namespace DtxFramework.Domain.Core.Events
{
	public abstract class Message : object, MediatR.IRequest<bool>
	{
		protected Message() : base()
		{
			Type =
				GetType().Name;
		}

		public string Type { get; protected set; }
	}
}
