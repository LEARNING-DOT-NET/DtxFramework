﻿using System.Linq;

namespace DtxFramework.Infrastructure.Bus
{
	public sealed class RabbitMQEventBus : object, Domain.Core.Bus.IEventBus
	{
		/// <summary>
		/// Updated!
		/// </summary>
		public RabbitMQEventBus
			(MediatR.IMediator mediator,
			Microsoft.Extensions.DependencyInjection.IServiceScopeFactory serviceScopeFactory) : base()
		{
			Mediator = mediator;

			// New!
			ServiceScopeFactory = serviceScopeFactory;

			EventTypes =
				new System.Collections.Generic.List<System.Type>();

			EventHandlerTypes =
				new System.Collections.Generic.Dictionary
				<string, System.Collections.Generic.List<System.Type>>();
		}

		private MediatR.IMediator Mediator { get; }

		private System.Collections.Generic.List<System.Type> EventTypes { get; }

		private System.Collections.Generic.Dictionary
			<string, System.Collections.Generic.List<System.Type>> EventHandlerTypes
		{ get; }

		/// <summary>
		/// New!
		/// </summary>
		private Microsoft.Extensions.DependencyInjection.IServiceScopeFactory ServiceScopeFactory { get; }

		public System.Threading.Tasks.Task
			SendCommand<TCommand>(TCommand command)
			where TCommand : Domain.Core.Commands.Command
		{
			return Mediator.Send(request: command);
		}

		public void Publish<TEvent>(TEvent @event)
			where TEvent : Domain.Core.Events.Event
		{
			try
			{
				var factory =
					new RabbitMQ.Client.ConnectionFactory
					{
						UserName = "guest",
						Password = "guest",
						HostName = "localhost",
					};

				string eventTypeName = @event.TypeName;

				string queueName = eventTypeName;
				string exchangeName = string.Empty;

				using (var connection = factory.CreateConnection())
				{
					using (var channel = connection.CreateModel())
					{
						channel.QueueDeclare
							(queue: queueName,
							durable: false,
							exclusive: false,
							autoDelete: false,
							arguments: null);

						string message =
							Newtonsoft.Json.JsonConvert.SerializeObject(@event);

						byte[] body =
							System.Text.Encoding.UTF8.GetBytes(message);

						channel.BasicPublish
							(exchange: exchangeName,
							routingKey: queueName,
							mandatory: false,
							basicProperties: null,
							body: body);

						System.Console.WriteLine($"Sent message: { message }");
					}
				}
			}
			catch (System.Exception ex)
			{
				System.Console.WriteLine(ex.Message);
			}
		}

		public void Subscribe<TEvent, TEventHandler>()
			where TEvent : Domain.Core.Events.Event
			where TEventHandler : Domain.Core.Bus.IEventHandler<TEvent>
		{
			try
			{
				string eventTypeName = typeof(TEvent).Name;
				System.Type eventHandlerType = typeof(TEventHandler);

				if (EventTypes.Contains(typeof(TEvent)) == false)
				{
					EventTypes.Add(typeof(TEvent));
				}

				if (EventHandlerTypes.ContainsKey(eventTypeName) == false)
				{
					EventHandlerTypes.Add(eventTypeName,
						new System.Collections.Generic.List<System.Type>());
				}

				if (EventHandlerTypes[eventTypeName].Any(current => current.GetType() == eventHandlerType))
				{
					string errorMessage =
						$"Handler Type '{ eventHandlerType.Name }' already is registered for '{ eventTypeName }'!";

					throw new System.ArgumentException
						(message: errorMessage, paramName: nameof(eventHandlerType));
				}

				EventHandlerTypes[eventTypeName].Add(eventHandlerType);

				StartBasicConsume<TEvent>();
			}
			catch (System.Exception ex)
			{
				System.Console.WriteLine(ex.Message);
			}
		}

		private void StartBasicConsume<TEvent>() where TEvent : Domain.Core.Events.Event
		{
			var factory =
				new RabbitMQ.Client.ConnectionFactory
				{
					UserName = "guest",
					Password = "guest",
					HostName = "localhost",
					DispatchConsumersAsync = true,
				};

			string eventTypeName = typeof(TEvent).Name;

			string queueName = eventTypeName;
			string exchangeName = string.Empty;

			using (var connection = factory.CreateConnection())
			{
				using (var channel = connection.CreateModel())
				{
					channel.QueueDeclare
						(queue: queueName,
						durable: false,
						exclusive: false,
						autoDelete: false,
						arguments: null);

					//var consumer =
					//	new RabbitMQ.Client.Events.EventingBasicConsumer(model: channel);

					var consumer =
						new RabbitMQ.Client.Events.AsyncEventingBasicConsumer(model: channel);

					consumer.Received += Consumer_Received;

					channel.BasicConsume
						(queue: queueName,
						autoAck: true,
						consumerTag: string.Empty, // null -> Runtime Error!
						noLocal: false,
						exclusive: false,
						arguments: null,
						consumer: consumer);
				}
			}
		}

		private async System.Threading.Tasks.Task Consumer_Received
			(object sender, RabbitMQ.Client.Events.BasicDeliverEventArgs e)
		{
			string queueName = e.RoutingKey;

			string eventTypeName = queueName;

			byte[] body = e.Body.ToArray();

			string message =
				System.Text.Encoding.UTF8.GetString(body);

			System.Console.WriteLine($"Received message: { message }");

			await ProcessEvent(eventTypeName, message).ConfigureAwait(false);
		}

		private async System.Threading.Tasks.Task ProcessEvent(string eventTypeName, string message)
		{
			if (EventHandlerTypes.ContainsKey(eventTypeName))
			{
				// New!
				using (var scope = ServiceScopeFactory.CreateScope())
				{
					System.Collections.Generic.List<System.Type>
						subscriptions = EventHandlerTypes[eventTypeName];

					foreach (var subscription in subscriptions)
					{
						//var eventHandler =
						//	System.Activator.CreateInstance(subscription);

						// Updated!
						var eventHandler =
							scope.ServiceProvider.GetService(subscription);

						if (eventHandler == null)
						{
							continue;
						}

						var eventType =
							EventTypes
							.Where(current => current.Name.ToLower() == eventTypeName.ToLower())
							.FirstOrDefault();

						var @event =
							Newtonsoft.Json.JsonConvert
							.DeserializeObject(value: message, type: eventType);

						var concreteType =
							typeof(Domain.Core.Bus.IEventHandler<>).MakeGenericType(typeArguments: eventType);

						await System.Threading.Tasks.Task.Run(() =>
						{
							concreteType.GetMethod("Handle").Invoke(eventHandler, new object[] { @event });
						});
					}
				}
			}
		}
	}
}
