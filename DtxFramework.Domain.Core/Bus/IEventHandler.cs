namespace DtxFramework.Domain.Core.Bus
{
	public interface IEventHandler
	{
	}

	public interface IEventHandler<TEvent> where TEvent : Events.Event
	{
		System.Threading.Tasks.Task Handle(TEvent @event);
	}
}
