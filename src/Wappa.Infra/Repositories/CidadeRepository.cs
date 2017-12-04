using System.Data;
using Wappa.Domain.Entities;
using Wappa.Domain.Repositories;
using Dapper;
using System.Linq;

namespace Wappa.Infra.Repositories
{
    internal class CidadeRepository : ICidadeRepository
    {
        const string PROC_SELECT_ID_CIDADE = @"PROC_SELECT_ID_CIDADE";
        const string PROC_SELECT_CIDADE = @"PROC_SELECT_CIDADE";

        private IDbConnection _connection;

        internal CidadeRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        Cidade[] ICidadeRepository.ListarTodos()
        {
            return _connection
                 .Query<Cidade, Estado, Cidade>(PROC_SELECT_CIDADE, commandType: CommandType.StoredProcedure, map:
                                (cidade, estado) =>
                                {
                                    cidade.Estado = estado;
                                    return cidade;
                                }, splitOn: "estadoId").ToArray();
        }

        Cidade ICidadeRepository.Obter(int id)
        {
            return _connection
                   .Query<Cidade, Estado, Cidade>(PROC_SELECT_ID_CIDADE, commandType: CommandType.StoredProcedure, map:
                                (cidade, estado) =>
                                {
                                    cidade.Estado = estado;
                                    return cidade;
                                }, param: new { id = id }, splitOn: "estadoId"
                    ).FirstOrDefault();
        }
    }
}
