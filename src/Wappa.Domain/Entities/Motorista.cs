using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Domain.Entities
{
    public class Motorista
    {
        public int MotoristaId { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public Carro Carro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public Cidade Cidade { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }       
    }
}
