using System;
using System.Collections.Generic;
using System.Text;
using Teste.Implementacao.Inteface;

namespace Teste.Implementacao
{
    public interface ICadastro
    {
        void Cadastrar(IDTO dadosCadastro);
        void Editar(IDTO dadosCadastro);
        void Excluir(int id);
        IEnumerable<IDTO> Consultar(IFiltro filtroConsulta);
    }
}
