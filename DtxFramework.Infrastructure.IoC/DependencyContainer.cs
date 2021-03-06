﻿using Microsoft.Extensions.DependencyInjection;

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
			//services.AddTransient
			//	<Domain.Core.Bus.IEventBus, Bus.RabbitMQEventBusSimple>();

			// Updated!
			services.AddSingleton
				<Domain.Core.Bus.IEventBus, Bus.RabbitMQEventBus>(sp =>
				{
					Microsoft.Extensions.DependencyInjection.IServiceScopeFactory serviceScopeFactory =
						sp.GetRequiredService<Microsoft.Extensions.DependencyInjection.IServiceScopeFactory>();

					return new Bus.RabbitMQEventBus(sp.GetService<MediatR.IMediator>(), serviceScopeFactory);
				});

			// Application Service(s)
			services.AddTransient
				<Banking.Application.Interfaces.IAccountService,
				Banking.Application.Services.AccountService>();

			// Data
			services.AddTransient
				<Banking.Data.Context.DatabaseContext>();

			services.AddTransient
				<Banking.Domain.Interfaces.IAccountRepository,
				Banking.Data.Repositories.AccountRepository>();
		}
	}
}
