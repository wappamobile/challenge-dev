using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Teste.API.Contratos
{
    public class Motorista
    {
        public int? Id { get;  set; }
        public string Nome { get;  set; }
        public string Sobrenome { get;  set; }
        public Carro Carro { get; set; }
        public Endereco Endereco { get; set; }
    }
}
