using Wappa.Middleware.Consts;
using Wappa.Middleware.Miscellaneous;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Wappa.Middleware.EntityFrameworkCore.Contexts
{
    public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var configuration = AppConfig.Get(Directory.GetParent(Directory.GetCurrentDirectory()).FullName + "\\Wappa.Middleware.Host", envName);

            DbContextConfigurer.Configure(builder, configuration.GetConnectionString(AppConsts.ConnectionStringName));
            
            return new AppDbContext(builder.Options);
        }
    }
}
