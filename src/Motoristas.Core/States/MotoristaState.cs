using System;

namespace Motoristas.Core.States
{
    public class MotoristaState
    {
        public string Nome { get; set; }
        public string UltimoNome { get; set; }
        public EnderecoState Endereco { get; set; }
        public VeiculoState Veiculo { get; set; }
    }
}
