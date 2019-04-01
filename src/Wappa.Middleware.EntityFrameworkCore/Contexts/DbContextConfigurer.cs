using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace Wappa.Middleware.EntityFrameworkCore.Contexts
{
    public class DbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AppDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString).UseLazyLoadingProxies();
        }

        public static void Configure(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseSqlServer(connectionString).UseLazyLoadingProxies();
        }

        public static void Configure(DbContextOptionsBuilder<AppDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection).UseLazyLoadingProxies();
        }
    }
}
