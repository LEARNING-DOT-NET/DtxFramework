using Microsoft.Extensions.DependencyInjection;

namespace DtxFramework.Infrastructure.IoC
{
	public static class DependencyContainer
	{
		static DependencyContainer()
		{
		}

		public static void RegisterServices
			(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
		{
			// Domain Bus
			services.AddTransient
				<Domain.Core.Bus.IEventBus, Bus.RabbitMQEventBus>();
		}
	}
}
