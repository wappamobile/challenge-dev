using Driver.Application.Enums;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Driver.Application.Data.Repositories.Common
{
    /// <summary>
    /// Estrutura para permitir trabalhar com connection em estrutura de escopo mantendo uma connection por chamada
    /// </summary>
    public class SqlConnectionProvider : IDisposable
    {
        private SqlConnection _connection;
        private readonly object state = new object();
        private readonly ConnectionEnum connectionName;

        /// <summary>
        /// Nova instancia padrão com a base default
        /// </summary>
        public SqlConnectionProvider() : this(ConnectionEnum.DefaultConnection)
        {
        }

        /// <summary>
        /// Nova instancia com uma base especifica
        /// </summary>
        /// <param name="connection"></param>
        public SqlConnectionProvider(ConnectionEnum connection)
        {
            connectionName = connection;
        }


        /// <summary>
        /// Connection atual. O processo de conexão é feito automaticamente aqui
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    lock (state)
                    {
                        if (_connection == null)
                        {
                            _connection = ConnectionManager.Create(connectionName);
                        }
                    }
                }

                if (_connection.State != ConnectionState.Open)
                {
                    lock (state)
                    {
                        if (_connection.State != ConnectionState.Open)
                        {
                            _connection.Open();
                        }
                    }
                }

                return _connection;
            }
        }

        #region IDisposable Support

        private bool disposedValue = false;

        /// <summary>
        /// Descarta a conexão
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _connection?.Close();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}