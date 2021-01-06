namespace DtxFramework.Domain.Core.Events
{
	public abstract class Message : Base, MediatR.IRequest<bool>
	{
		protected Message() : base()
		{
		}
	}
}
