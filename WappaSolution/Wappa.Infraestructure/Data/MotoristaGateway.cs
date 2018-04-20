using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Wappa.Domain.Entidades;
using Wappa.Domain.Services;

namespace Wappa.Infraestructure.Data
{
    public class MotoristaGateway : IMotoristaGateway
    {
        private readonly DatabaseContext databaseContext;

        /// <summary>
        /// Construtor da classe <see cref="MotoristaGateway"/>
        /// </summary>
        /// <param name="databaseContext">Contexto da base de dados <see cref="DatabaseContext"/></param>
        public MotoristaGateway(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        /// <summary>
        /// Obtem todos os motoristas
        /// </summary>
        /// <returns>Retorna a lista de motoristas</returns>
        public IEnumerable<Motorista> ObterTodos()
        {
            return databaseContext.Motoristas.Include(m => m.Endereco)
                                             .Include(m => m.Veiculo);
        }

        /// <summary>
        /// Obtem um motorista pelo ID
        /// </summary>
        /// <param name="motoristaID">Id do motorista</param>
        /// <returns>Entidade Motorista</returns>
        public Motorista ObterPorId(int motoristaID)
        {
            return this.ObterTodos().Where(m => m.MotoristaID.Equals(motoristaID)).SingleOrDefault();
        }

        /// <summary>
        /// Obter lista de motorista Ordenado pelo primeiro nome.
        /// </summary>
        /// <returns>Lista de Motorista</returns>
        public IEnumerable<Motorista> ObterOrdenadoPorPrimeiroNome()
        {
            return this.ObterTodos().OrderBy(m => m.PrimeiroNome).ToList();
        }

        /// <summary>
        /// Obter lista de motorista Ordenado pelo ultimo nome.
        /// </summary>
        /// <returns>Lista de Motorista</returns>
        public IEnumerable<Motorista> ObterOrdenadoPorUltimoNome()
        {
            return this.ObterTodos().OrderBy(m => m.UltimoNome).ToList();
        }

        /// <summary>
        /// Incluir Novo Motorista
        /// </summary>
        /// <param name="motorista">Entidade Motorista</param>
        public void Novo(Motorista motorista)
        {
            this.databaseContext.Motoristas.Add(motorista);

            this.databaseContext.SaveChanges();
        }

        /// <summary>
        /// Alterar Motorista
        /// </summary>
        /// <param name="motorista">Entidade Motorista</param>
        public void Alterar(Motorista motorista)
        {
            this.databaseContext.Motoristas.Update(motorista);

            this.databaseContext.SaveChanges();
        }

        /// <summary>
        /// Excluir motorista
        /// </summary>
        /// <param name="motoristaID">ID do Motorista</param>
        public void Excluir(int motoristaID)
        {
            var motorista = this.ObterPorId(motoristaID);

            this.databaseContext.Veiculos.Remove(motorista.Veiculo);
            this.databaseContext.Enderecos.Remove(motorista.Endereco);
            this.databaseContext.Motoristas.Remove(motorista);

            this.databaseContext.SaveChanges();
        }
    }
}
