using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Wappa.Infra.Data.Repositories
{
    public abstract class Repository
    {
        private const string ConnectionStringName = "Database";

        protected IDbConnection Connection;
        protected string ConnectionString { get; }

        protected Repository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString(ConnectionStringName);
        }

        protected IDbConnection CreateConnection() =>
            new SqlConnection(ConnectionString);
    }
}