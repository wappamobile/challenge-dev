using Wappa.Middleware.Consts;
using Wappa.Middleware.Miscellaneous;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Wappa.Middleware.EntityFrameworkCore.Contexts
{
    public class DbContextOptionsBuilder
    {
        public static DbContextOptions<AppDbContext> Get()
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var configuration = AppConfig.Get(Directory.GetCurrentDirectory(), envName);

            var builder = new DbContextOptionsBuilder<AppDbContext>();

            DbContextConfigurer.Configure(builder, configuration.GetConnectionString(AppConsts.ConnectionStringName));

            return builder.Options;
        }
    }
}
