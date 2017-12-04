using System;
using Wappa.Domain.UnitOfWork;
using System.Data;
using MySql.Data.MySqlClient;
using Wappa.Domain.Repositories;
using Wappa.Infra.Repositories;

namespace Wappa.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly IDbConnection _connection;
        private readonly MotoristaRepository _motoristaRepository;
        private readonly CarroRepository _carroRepository;
        private readonly CidadeRepository _cidadeRepository;
        private IDbTransaction _transaction;

        public IMotoristaRepository GetMotoristaRepository()
        {
            return _motoristaRepository;
        }

        public ICarroRepository GetCarroRepository()
        {
            return _carroRepository;
        }

        public ICidadeRepository GetCidadeRepository()
        {
            return _cidadeRepository;
        }

        public UnitOfWork(string connectionString)
        {
            _connection = new MySqlConnection(connectionString);
            _connection.Open();
            _motoristaRepository = new MotoristaRepository(_connection);
            _carroRepository = new CarroRepository(_connection);
            _cidadeRepository = new CidadeRepository(_connection);
        }

        public void BeginTransaction()
        {
            _transaction = _connection.BeginTransaction();
        }


        public void Commit()
        {
            if (_transaction == null) return;

            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                Rollback();
                // TODO: logar a exception
            }
        }

        public void Rollback()
        {
            if (_transaction == null) return;

            _transaction.Rollback();
        }

        #region IDisposable Support
        private bool disposedValue = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_connection.State != ConnectionState.Closed)
                        _connection.Close();

                    _connection.Dispose();
                }                
                disposedValue = true;
            }
        }
        
        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
