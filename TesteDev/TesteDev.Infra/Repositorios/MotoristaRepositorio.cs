using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TesteDev.Infra.Entidades;
using TesteDev.Infra.Repositorios.Interfaces;
using TesteDev.Servicos.Interfaces;

namespace TesteDev.Infra.Repositorios
{
    internal class MotoristaRepositorio : RepositorioBase<Motorista>, IMotoristaRepositorio
    {
        private readonly Contexto _contexto;
        private readonly ILocalizacaoServico _localizacaoServico;

        /// <summary>
        /// Contrutor que recebe parametros por DI
        /// </summary>
        /// <param name="contexto">Contexto</param>
        /// <param name="localizacaoServico">Servico para buscar localização</param>
        public MotoristaRepositorio(Contexto contexto, ILocalizacaoServico localizacaoServico) : base(contexto)
        {
            _contexto = contexto;
            _localizacaoServico = localizacaoServico;
        }

        /// <summary>
        /// Cria um novo motorista no banco de dados
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns>Retorna motorista criado</returns>
        public override Motorista Criar(Motorista entidade)
        {
            PreencherLocalizacao(entidade);

            return base.Criar(entidade);
        }

        /// <summary>
        /// Atualiza motorista no banco de dados
        /// </summary>
        /// <param name="entidade">Entidade a ser atualizada</param>
        /// <returns>Retorna motorista atualizado</returns>
        public override Motorista Atualizar(Motorista entidade)
        {
            PreencherLocalizacao(entidade);
            this._contexto.Entry(entidade.EnderecoCompleto).State = EntityState.Modified;
            this._contexto.Entry(entidade.Carro).State = EntityState.Modified;

            return base.Atualizar(entidade);
        }

        /// <summary>
        /// Lista os motoristas cadastrados de acordo com Nome OU ultimo nome
        /// </summary>
        /// <param name="nome">Nome</param>
        /// <param name="ultimoNome">Ultimo nome</param>
        /// <returns>Retorna lista ordenada dos motoristas</returns>
        public IList<Motorista> Listar(string nome, string ultimoNome)
        {
            return base.Entidades.Include(e => e.Carro)
                                 .Include(e => e.EnderecoCompleto)
                                 .Include(e => e.Localizacao)
                                 .Where(e => e.Ativo == true && ((!string.IsNullOrEmpty(nome) && e.Nome.Contains(nome))
                                            || (!string.IsNullOrEmpty(ultimoNome) && e.UltimoNome.Contains(ultimoNome))
                                            || (string.IsNullOrEmpty(nome) && string.IsNullOrEmpty(ultimoNome)))
                                            ).OrderBy(o => o.Nome).ThenBy(o => o.UltimoNome).ToList();
        }

        /// <summary>
        /// Método responsável por preencher a localização
        /// </summary>
        /// <param name="entidade">Entidade a ser preenchida</param>
        private void PreencherLocalizacao(Motorista entidade)
        {
            if (entidade.EnderecoCompleto != null)
            {
                string enderecoFormatado = string.Format("{0}, {1}", entidade.EnderecoCompleto.Endereco, entidade.EnderecoCompleto.Numero);
                entidade.Localizacao = _localizacaoServico.RetornarLocalizacao(enderecoFormatado);
            }
        }
    }
}
