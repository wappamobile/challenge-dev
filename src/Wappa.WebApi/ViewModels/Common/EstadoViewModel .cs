using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Domain.Entities;

namespace Wappa.WebApi.ViewModels.Common
{
    public class EstadoViewModel
    {
        public int EstadoId { get; private set; }
        public string Descricao { get; private set; }
        public string Sigla { get; private set; }

        public EstadoViewModel(Estado estado)
        {
            this.EstadoId = estado.EstadoId;
            this.Descricao = estado.Descricao;
            this.Sigla = estado.Sigla;
        }
    }
}
