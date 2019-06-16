using System.Globalization;
using MicroservicesPlatform.Configuration;
using MicroservicesPlatform.Middlewares;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Motoristas.Modules;
using Nancy.Owin;
using Serilog;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace Motoristas
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfiguration _configuration;

        public Startup(IHostingEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            if (!_env.IsDevelopment())
            {
                BasePathMapping.BasePath = "/motoristas";
            }


            void ConfigureAuthenticationOptions(AuthenticationOptions options)
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }

            void ConfigureJwtBearerOptions(JwtBearerOptions options)
            {
                var authConfig = new AuthenticationConfig();
                _configuration.GetSection("Auth").Bind(authConfig);
                options.Authority = authConfig.Authority;
                options.Audience = authConfig.Audience;
                options.RequireHttpsMetadata = true;
            }

            services.AddCors()
                    .AddAuthentication(ConfigureAuthenticationOptions)
                    .AddJwtBearer(ConfigureJwtBearerOptions);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var corsConfig = new CORSConfig();
            _configuration.GetSection("CORS").Bind(corsConfig);

            void ConfigureCorsPolicy(CorsPolicyBuilder builder) => builder.WithOrigins(corsConfig.AllowOrigin)
                                                                          .AllowCredentials()
                                                                          .AllowAnyMethod()
                                                                          .AllowAnyHeader();

            var logLevel = _env.IsDevelopment()
                               ? LogEventLevel.Debug
                               : LogEventLevel.Information;

            var formatter = _env.IsDevelopment()
                                ? (ITextFormatter)new MessageTemplateTextFormatter("{NewLine}{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3} {CorrelationToken} {Message}{NewLine}{Exception}", CultureInfo.InvariantCulture)
                                : new ElasticsearchJsonFormatter();

            var log = LoggerFactory.CreateLogger(logLevel, formatter);

            app.UseCors(ConfigureCorsPolicy)
               .UseOwin()
               .UseLogging(log)
               .UseNancy(o => o.Bootstrapper = new Bootstrapper(log, _configuration));
        }
    }
    public static class LoggerFactory
    {
        public static ILogger CreateLogger(LogEventLevel logLevel, ITextFormatter formatter)
        {
            return new LoggerConfiguration()
                   .MinimumLevel.Is(logLevel)
                   .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                   .Enrich.FromLogContext()
                   .WriteTo.Console(formatter)
                   .CreateLogger();
        }
    }
}
