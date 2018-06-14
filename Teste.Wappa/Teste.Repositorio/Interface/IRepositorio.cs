using System;
using System.Collections.Generic;
using System.Text;

namespace Teste.Repositorio.Interface
{
    public interface IRepositorio<IEntidade>
    {
        IEnumerable<IEntidade> Consultar();
        void Excluir(int id);
        void Inserir(IEntidade dados);
        void Editar(IEntidade dados);
    }
}
