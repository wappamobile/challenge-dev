using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Wappa.Dominio.Entidade;
using Wappa.ViewModel.Motorista;

namespace Wappa.Dominio.Repositorio
{
    public interface IMotoristaRepositorio : IRepositorio<Motorista>
    {
        #region Apresentação

        List<MotoristaVM> ObterTodos();

        #endregion Apresentação

        Motorista Obter(Expression<Func<Motorista, bool>> condicao);
    }
}