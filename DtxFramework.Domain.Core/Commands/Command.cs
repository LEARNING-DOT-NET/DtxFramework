namespace DtxFramework.Domain.Core.Commands
{
	public abstract class Command : Base.Entity, MediatR.IRequest<bool>
	{
		protected Command() : base()
		{
		}
	}
}
