using System.Collections.Generic;
using System.Data;
using Wappa.Domain.Entities;
using Wappa.Domain.Repositories;
using Dapper;
using System.Linq;
using Wappa.Domain.Common;
using System;

namespace Wappa.Infra.Repositories
{
    internal class MotoristaRepository : IMotoristaRepository
    {
        const string PROC_SELECT_MOTORISTA = @"PROC_SELECT_MOTORISTA";

        const string PROC_SELECT_ID_MOTORISTA = @"PROC_SELECT_ID_MOTORISTA";

        const string PROC_INSERT_MOTORISTA = @"PROC_INSERT_MOTORISTA";

        const string PROC_UPDATE_MOTORISTA = @"PROC_UPDATE_MOTORISTA";

        const string PROC_DELETE_MOTORISTA = @"PROC_DELETE_MOTORISTA";


        private IDbConnection _connection;
        
        internal MotoristaRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        Motorista IMotoristaRepository.Adicionar(Motorista motorista)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@P_NOME", motorista.Nome);
            parametros.Add("@P_SOBRENOME", motorista.Sobrenome);
            parametros.Add("@P_CARROID", motorista.Carro.CarroId);
            parametros.Add("@P_LOGRADOURO", motorista.Logradouro);
            parametros.Add("@P_NUMERO", motorista.Numero);
            parametros.Add("@P_BAIRRO", motorista.Bairro);
            parametros.Add("@P_CEP", motorista.Cep);
            parametros.Add("@P_CIDADEID", motorista.Cidade.CidadeId);
            parametros.Add("@P_LATITUDE", motorista.Latitude);
            parametros.Add("@P_LONGITUDE", motorista.Longitude);

            motorista.MotoristaId = _connection.QuerySingle<int>(PROC_INSERT_MOTORISTA, parametros, commandType: CommandType.StoredProcedure);

            return motorista;
        }

        Motorista IMotoristaRepository.Atualizar(Motorista motorista)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@P_ID", motorista.MotoristaId);
            parametros.Add("@P_NOME", motorista.Nome);
            parametros.Add("@P_SOBRENOME", motorista.Sobrenome);
            parametros.Add("@P_CARROID", motorista.Carro.CarroId);
            parametros.Add("@P_LOGRADOURO", motorista.Logradouro);
            parametros.Add("@P_NUMERO", motorista.Numero);
            parametros.Add("@P_BAIRRO", motorista.Bairro);
            parametros.Add("@P_CEP", motorista.Cep);
            parametros.Add("@P_CIDADEID", motorista.Cidade.CidadeId);
            parametros.Add("@P_LATITUDE", motorista.Latitude);
            parametros.Add("@P_LONGITUDE", motorista.Longitude);

            _connection.Query<int>(PROC_UPDATE_MOTORISTA, parametros, commandType: CommandType.StoredProcedure);

            return motorista;
        }

        void IMotoristaRepository.Excluir(int id)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@ID", id);

            _connection.Query<int>(PROC_DELETE_MOTORISTA, parametros, commandType: CommandType.StoredProcedure);
        }

        Motorista[] IMotoristaRepository.Listar(OrdenarPor ordernarPor)
        {
            return _connection
                    .Query<Motorista, Carro, Cidade, Estado, Motorista>(PROC_SELECT_MOTORISTA, commandType: CommandType.StoredProcedure, map:
                                (motorista, carro, cidade, estado) =>
                                {
                                    motorista.Carro = carro;
                                    motorista.Cidade = cidade;
                                    motorista.Cidade.Estado = estado;
                                    return motorista;
                                }, splitOn: "carroId, cidadeId, estadoId", param: new { ORDER_PARAMETER = Convert.ToInt32(ordernarPor) }
                    ).ToArray();
        }

        Motorista IMotoristaRepository.Obter(int id)
        {
            return _connection
                    .Query<Motorista, Carro, Cidade, Estado, Motorista>(PROC_SELECT_ID_MOTORISTA, commandType: CommandType.StoredProcedure, map:
                                (motorista, carro, cidade, estado) => 
                                        {
                                            motorista.Carro = carro;
                                            motorista.Cidade = cidade;
                                            motorista.Cidade.Estado = estado;
                                            return motorista;
                                        }, param: new { id = id }, splitOn: "carroId, cidadeId, estadoId"
                    ).FirstOrDefault();
        }
    }
}
