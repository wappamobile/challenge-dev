using System;
using System.Collections.Generic;
using System.Text;

namespace Wappa.Domain.Entidades
{
    public class Endereco
    {
        public int EnderecoID { get; set; }

        public string Logradouro { get; set; }

        public string Numero { get; set; }

        public string Complemento { get; set; }

        public string Bairro { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }
    }
}
