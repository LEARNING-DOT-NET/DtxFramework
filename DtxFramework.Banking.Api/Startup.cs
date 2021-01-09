using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DtxFramework.Banking.Api
{
	public class Startup : object
	{
		public Startup(Microsoft.Extensions.Configuration.IConfiguration configuration) : base()
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

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
