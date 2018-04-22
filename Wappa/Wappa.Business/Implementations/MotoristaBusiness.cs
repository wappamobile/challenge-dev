using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wappa.Business.Interfaces;
using Wappa.DataAccess.Interfaces;
using Wappa.Models;
using Wappa.Models.Enum;

namespace Wappa.Business.Implementations
{
    public class MotoristaBusiness : IMotoristaBusiness
    {
        private readonly ILocalizacaoBusiness localizacaoBusiness;
        private readonly IMotoristaRepository motoristaRepository;

        public MotoristaBusiness(ILocalizacaoBusiness localizacaoBusiness,
            IMotoristaRepository motoristaRepository)
        {
            this.localizacaoBusiness = localizacaoBusiness;
            this.motoristaRepository = motoristaRepository;
        }

        public Motorista ObterPorId(int id)
        {
            return motoristaRepository.ObterPorId(id);
        }

        public IEnumerable<Motorista> Listar(CampoOrdenacaoEnum? ordenacao = CampoOrdenacaoEnum.Nenhum)
        {
            switch (ordenacao)
            {
                case CampoOrdenacaoEnum.Nome:
                    return motoristaRepository.ListarTodos().OrderBy(m => m.Nome);
                case CampoOrdenacaoEnum.UltimoNome:
                    return motoristaRepository.ListarTodos().OrderBy(m => m.UltimoNome);
                default:
                    return motoristaRepository.ListarTodos();
            }
        }

        public async Task Incluir(Motorista motorista)
        {
            var localizacao = await localizacaoBusiness.ObterCoordenadas(motorista.Endereco);
            motorista.Endereco.Latitude = localizacao.Latitude.ToString();
            motorista.Endereco.Longitude = localizacao.Longitude.ToString();

            motoristaRepository.Incluir(motorista);
        }

        public async Task<bool> Alterar(Motorista motorista)
        {
            var localizacao = await localizacaoBusiness.ObterCoordenadas(motorista.Endereco);
            motorista.Endereco.Latitude = localizacao.Latitude.ToString();
            motorista.Endereco.Longitude = localizacao.Longitude.ToString();

            return motoristaRepository.Alterar(motorista);
        }

        public bool Excluir(int id)
        {
            return motoristaRepository.Excluir(id);
        }
    }
}
