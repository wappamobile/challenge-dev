using Wappa.Middleware.Consts;
using Wappa.Middleware.Miscellaneous;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Wappa.Middleware.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static void Configure(this DbContextOptionsBuilder options)
        {
            //var configuration = AppConfig.Get(FolderFinder.FetchRoot());
            //string connString = configuration.GetConnectionString(AppConsts.ConnectionStringName);

            var envName = Environment.GetEnvironmentVariable("LIS_ENVIRONMENT");

            var directoryInfo =  Directory.GetCurrentDirectory();
            var builder = new ConfigurationBuilder()
                .SetBasePath(directoryInfo)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .Build();

            string connString = builder.GetConnectionString(AppConsts.ConnectionStringName);

            options.UseSqlServer(connString).UseLazyLoadingProxies();
        }

        public static void ConfigureNoLazyLoading(this DbContextOptionsBuilder options)
        {
            //var configuration = AppConfig.Get(FolderFinder.FetchRoot());
            //string connString = configuration.GetConnectionString(AppConsts.ConnectionStringName);

            var envName = Environment.GetEnvironmentVariable("LIS_ENVIRONMENT");

            var directoryInfo =  Directory.GetCurrentDirectory();//Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName).FullName;
            var builder = new ConfigurationBuilder()
                .SetBasePath(directoryInfo)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true)
                .Build();

            string connString = builder.GetConnectionString(AppConsts.ConnectionStringName);

            options.UseSqlServer(connString);
        }
    }
}
