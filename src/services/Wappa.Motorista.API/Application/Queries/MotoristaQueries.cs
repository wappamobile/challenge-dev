using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Wappa.Motoristas.API.Application.DTO;
using Wappa.Motoristas.API.Models;

namespace Wappa.Motoristas.API.Application.Queries
{
    public interface IMotoristaQueries
    {
        Task<IEnumerable<MotoristaDTO>> ObterListaMotoristas();
    }

    public class MotoristaQueries : IMotoristaQueries
    {
        #region Sql
        const string _sql_Motoristas = @"SELECT
													M.ID,
													M.NOME,
													M.SOBRENOME,
													E.LOGRADOURO,
													E.NUMERO, 
													E.BAIRRO, 
													E.CEP, 
													E.COMPLEMENTO, 
													E.CIDADE, 
													E.ESTADO,
													E.LATITUDE,
                                                    E.LONGITUDE,
													C.MARCA,
													C.MODELO,
													C.PLACA
												FROM 
													MOTORISTAS M
												INNER JOIN 
													ENDERECOS E
												ON E.MOTORISTAID = M.ID
												INNER JOIN 
													CARROS C
												ON C.MOTORISTAID = M.ID 
                                ORDER BY M.NOME";
        #endregion
        private readonly IMotoristaRepository  _motoristaRepository;

		public MotoristaQueries(IMotoristaRepository motoristaRepository)
		{
			_motoristaRepository = motoristaRepository;
		}
		
        public async Task<IEnumerable<MotoristaDTO>> ObterListaMotoristas()
        {
			var motorista = await _motoristaRepository.ObterConexao()
				.QueryAsync<dynamic>(_sql_Motoristas);

			return MotoristaDTO.ParaMotoristaDTO(motorista);
        }
    }
}