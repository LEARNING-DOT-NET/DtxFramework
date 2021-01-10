using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DtxFramework.Banking.Api
{
	public class Startup : object
	{
		public Startup
			(Microsoft.Extensions.Configuration.IConfiguration configuration) : base()
		{
			Configuration = configuration;
		}

		public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

		public void ConfigureServices
			(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
		{
			services.AddControllers();

			services.AddDbContext<Banking.Data.Context.DatabaseContext>(options =>
			{
				// UseSqlServer -> using Microsoft.EntityFrameworkCore;
				// GetConnectionString -> using Microsoft.Extensions.Configuration;
				options.UseSqlServer
					(Configuration.GetConnectionString("BankingConnectionString"));
			});

			services.AddSwaggerGen(current =>
			{
				current.SwaggerDoc(name: "v1",
					info: new Microsoft.OpenApi.Models.OpenApiInfo
					{
						Version = "v1",
						Title = "Banking Microservice",
					});
			});

			services.AddMediatR(handlerAssemblyMarkerTypes: typeof(Startup));

			Infrastructure.IoC.DependencyContainer.RegisterServices(services: services);
		}

		public void Configure
			(Microsoft.AspNetCore.Builder.IApplicationBuilder app,
			Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseSwagger();

			app.UseSwaggerUI(current =>
			{
				current.SwaggerEndpoint
					(url: "/swagger/v1/swagger.json", name: "Banking Microservice V1");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			ConfigureEventBus(app);
		}

		private void ConfigureEventBus
			(Microsoft.AspNetCore.Builder.IApplicationBuilder app)
		{
			var eventBus =
				app.ApplicationServices.GetRequiredService<DtxFramework.Domain.Core.Bus.IEventBus>();

			// For Example!
			//eventBus.Subscribe<LogCreatedEvent, LogEventHandler>();
		}
	}
}
