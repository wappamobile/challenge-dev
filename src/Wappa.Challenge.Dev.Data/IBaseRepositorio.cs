using System.Collections.Generic;

namespace Wappa.Challenge.Dev.Data
{
    public interface IBaseRepositorio<T> where T : class
    {
        /// <summary>
        /// Salva a entidade
        /// </summary>
        /// <param name="entidade"></param>
        /// <returns>Quantidade de registros afetados</returns>
        int Salvar(T entidade);

        /// <summary>
        /// Exclui a entidade
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Quantidade de registros afetados</returns>
        int Excluir(int id);

        /// <summary>
        /// Objeto de acesso à listagem de entidades
        /// </summary>
        IEnumerable<T> Queryable { get; }

        /// <summary>
        /// Retorna entidade de acordo com o ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        T Obter(int ID);
    }
}
