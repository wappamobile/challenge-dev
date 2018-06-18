using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entity
{
    public class Endereco
    {
        public int EnderecoId { get; set; }
        public int? GeoLocationId { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string UF { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        public GeoLocation GeoLocation { get; set; }
    }
}
