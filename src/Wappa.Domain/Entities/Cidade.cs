using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Domain.Entities
{
    public class Cidade
    {
        public int CidadeId { get; set; }
        public string Nome { get; set; }
        public Estado Estado { get; set; }
    }
}
