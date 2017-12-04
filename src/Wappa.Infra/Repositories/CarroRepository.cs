using System.Data;
using Wappa.Domain.Entities;
using Wappa.Domain.Repositories;
using Dapper;
using System.Linq;

namespace Wappa.Infra.Repositories
{
    internal class CarroRepository : ICarroRepository
    {
        const string PROC_SELECT_CARRO = @"PROC_SELECT_CARRO";

        const string PROC_SELECT_ID_CARRO = @"PROC_SELECT_ID_CARRO";
                
        private IDbConnection _connection;
        
        internal CarroRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        Carro[] ICarroRepository.ListarTodos()
        {
            return _connection
                   .Query<Carro>(PROC_SELECT_CARRO, commandType: CommandType.StoredProcedure).ToArray();
        }

        Carro ICarroRepository.Obter(int id)
        {
            return _connection
                  .Query<Carro>(PROC_SELECT_ID_CARRO, commandType: CommandType.StoredProcedure, param: new { id = id }).FirstOrDefault();
        }
    }
}
