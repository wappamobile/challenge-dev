using Npgsql;
using System.Data;
using Vitor.Application.Options;

namespace Vitor.Infrastructure.Repositories
{
    public class Repository
    {
        private readonly PostgresOptions _options;

        public Repository(PostgresOptions options)
        {
            this._options = options;
        }

        protected virtual IDbConnection CreateConnection()
        {
            var teste = this._options.Password;
            var connection = new NpgsqlConnection(string.Format("Server={0}; User Id={1}; Password={2}; Database={3};",
                                                                this._options.Server,
                                                                this._options.UserId,
                                                                this._options.Password,
                                                                this._options.Database));
            connection.Open();
            return connection;
        }
    }
}
