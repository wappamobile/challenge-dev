using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Wappa.Api.DataLayer;
using Wappa.Api.ExternalServices;

namespace Wappa.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			var databaseConnection = this.Configuration.GetConnectionString("development");
			services.AddDbContext<BackOfficeContext>(options => options.UseSqlServer(databaseConnection));

			services.AddAutoMapper(cfg => cfg.AddProfile<MappingProfile>());

			

			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddScoped<IGoogleGeocoderWrapper, GoogleGeocoderWrapper>();

			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
