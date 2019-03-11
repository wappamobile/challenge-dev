using Driver.Application.Enums;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Driver.Application.Data.Repositories.Common
{
    /// <summary>
    /// Estrutura de gerenciammento de conexões com o banco
    /// </summary>
    public class ConnectionManager
    {
        /// <summary>
        /// Chave no app settings
        /// </summary>
        private const string ConnectionStringsKey = "ConnectionStrings";

        /// <summary>
        /// ConnectionStrings incluidas no appsettings
        /// </summary>
        internal static readonly IDictionary<ConnectionEnum, string> Connections;

        static ConnectionManager()
        {
            Connections = new Dictionary<ConnectionEnum, string>();

            var section = AppSettings.GetSection(ConnectionStringsKey);

            foreach (ConnectionEnum item in Enum.GetValues(typeof(ConnectionEnum)))
            {
                Connections.Add(item, section[item.ToString()]);
            }
        }

        /// <summary>
        /// Cria uma nova connection com connection string vinculada com o enum
        /// </summary>
        /// <param name="connection">Enum com o nome da connection</param>
        /// <returns></returns>
        public static SqlConnection Create(ConnectionEnum connection)
        {
            return new SqlConnection(Connections[connection]);
        }
    }
}