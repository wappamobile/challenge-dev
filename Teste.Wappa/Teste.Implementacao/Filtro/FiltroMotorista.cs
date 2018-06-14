using System;
using System.Collections.Generic;
using System.Text;
using Teste.Implementacao.Inteface;

namespace Teste.Implementacao.Filtro
{
    public enum OrdenacaoListaMotorista
    {
        Nome = 1,
        Sobrenome = 2
    }

    public class FiltroMotorista : IFiltro
    {
        private OrdenacaoListaMotorista ordenacao = OrdenacaoListaMotorista.Nome;
        public OrdenacaoListaMotorista OrdenarMotoristaPor { get => ordenacao; set => ordenacao = value; }
    }
}
