using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wappa.Domain.Entities;

namespace Wappa.WebApi.ViewModels.Common
{
    public class CidadeViewModel
    {
        public int CidadeId { get; private set; }
        public string Nome { get; private set; }
        public EstadoViewModel Estado { get; private set; }

        public CidadeViewModel(Cidade cidade)
        {
            this.CidadeId = cidade.CidadeId;
            this.Nome = cidade.Nome;
            this.Estado = new EstadoViewModel(cidade.Estado);
        }
    }
}
