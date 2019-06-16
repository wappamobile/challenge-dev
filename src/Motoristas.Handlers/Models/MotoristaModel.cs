using System;
using System.Collections.Generic;
using System.Text;

namespace Motoristas.Handlers.Models
{
    public class MotoristaModel
    {
        public string Nome { get; set; }
        public string UltimoNome { get; set; }
        public VeiculoModel Veiculo { get; set; }
        public EnderecoModel Endereco { get; set; }
    }
}
