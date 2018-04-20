using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Domain.Entidades
{
    public class Motorista
    {
        public int MotoristaID { get; set; }

        public string PrimeiroNome { get; set; }

        public string UltimoNome { get; set; }

        public int EnderecoID { get; set; }

        public int VeiculoID { get; set; }

        public Endereco Endereco { get; set; }

        public Veiculo Veiculo { get; set; }
    }
}
