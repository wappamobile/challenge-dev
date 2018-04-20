using System;
using System.Collections.Generic;
using System.Text;
using Wappa.Domain.Entidades;

namespace Wappa.Domain.Services
{
    public class MotoristaService : IMotoristaService
    {
        private readonly IMotoristaGateway motoristaGateway;
        private readonly ILocalizacaoGateway localizacaoGateway;

        /// <summary>
        /// Construtor da clase <see cref="MotoristaService"/>
        /// </summary>
        /// <param name="motoristaGateway">Interface Gateway para <see cref="IMotoristaGateway"/></param>
        /// <param name="localizacaoGateway">Interface Gateay para <see cref="ILocalizacaoGateway"/></param>
        public MotoristaService(IMotoristaGateway motoristaGateway,
                                ILocalizacaoGateway localizacaoGateway)
        {
            this.motoristaGateway = motoristaGateway;
            this.localizacaoGateway = localizacaoGateway;
        }

        /// <summary>
        /// Obtem um motorista pelo ID
        /// </summary>
        /// <param name="motoristaID">Id do motorista</param>
        /// <returns>Entidade Motorista</returns>
        public Motorista ObterPorId(int motoristaID)
        {
            return this.motoristaGateway.ObterPorId(motoristaID);
        }

        /// <summary>
        /// Obter lista de motorista Ordenado pelo primeiro nome.
        /// </summary>
        /// <returns>Lista de Motorista</returns>
        public IEnumerable<Motorista> ObterOrdenadoPorPrimeiroNome()
        {
            return this.motoristaGateway.ObterOrdenadoPorPrimeiroNome();
        }

        /// <summary>
        /// Obter lista de motorista Ordenado pelo ultimo nome.
        /// </summary>
        /// <returns>Lista de Motorista</returns>
        public IEnumerable<Motorista> ObterOrdenadoPorUltimoNome()
        {
            return this.motoristaGateway.ObterOrdenadoPorUltimoNome();
        }

        /// <summary>
        /// Incluir Novo Motorista
        /// </summary>
        /// <param name="motorista">Entidade Motorista</param>
        public void Novo(Motorista motorista)
        {
            var loc = this.ObterCoordenadas(motorista);

            motorista.Endereco.Latitude = loc.GetValueOrDefault("lat");
            motorista.Endereco.Longitude = loc.GetValueOrDefault("lng");

            this.motoristaGateway.Novo(motorista);
        }

        /// <summary>
        /// Alterar Motorista
        /// </summary>
        /// <param name="motorista">Entidade Motorista</param>
        public void Alterar(Motorista motorista)
        {
            var loc = this.ObterCoordenadas(motorista);

            motorista.Endereco.Latitude = loc.GetValueOrDefault("lat");
            motorista.Endereco.Longitude = loc.GetValueOrDefault("lng");

            this.motoristaGateway.Alterar(motorista);
        }

        /// <summary>
        /// Excluir motorista
        /// </summary>
        /// <param name="motoristaID">ID do Motorista</param>
        public void Excluir(int motoristaID)
        {
            this.motoristaGateway.Excluir(motoristaID);
        }

        private Dictionary<string, string> ObterCoordenadas(Motorista motorista)
        {
            return localizacaoGateway.ObterCoordenadas(motorista.Endereco.Logradouro + ", " +
                                                          motorista.Endereco.Numero + ", " +
                                                          motorista.Endereco.Cidade + ", " +
                                                          motorista.Endereco.Estado);
        }
    }
}
