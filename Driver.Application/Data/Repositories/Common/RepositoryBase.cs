using System.Data.SqlClient;

namespace Driver.Application.Data.Repositories.Common
{
    /// <summary>
    /// Estrutura base de repository
    /// </summary>
    public abstract class RepositoryBase
    {
        /// <summary>
        /// Conection enviada por parametro
        /// </summary>
        private SqlConnectionProvider _connection;

        /// <summary>
        /// Nova instancia do reposity
        /// </summary>
        /// <param name="connection">Conection que o repository em questão vai usar</param>
        public RepositoryBase(SqlConnectionProvider connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Connection atual do provider
        /// </summary>
        protected SqlConnection Connection => _connection.Connection;

        /// <summary>
        /// Cria um novo comando
        /// </summary>
        /// <returns></returns>
        protected SqlCommand CreateCommand()
        {
            var command = Connection.CreateCommand();

            command.CommandTimeout = AppSettings.CommandTimeout;

            return command;
        }
    }
}