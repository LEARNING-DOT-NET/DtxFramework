namespace DtxFramework.Domain.Core.Bus
{
	public interface IEventBus
	{
		void Publish<TEvent>(TEvent @event)
			where TEvent : Events.Event;

		void Subscribe<TEvent, TEventHandler>()
			where TEvent : Events.Event
			where TEventHandler : IEventHandler<TEvent>;

		System.Threading.Tasks.Task SendCommand<TCommand>(TCommand command)
			where TCommand : Commands.Command;
	}
}
