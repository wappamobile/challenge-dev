using System;
using System.Collections.Generic;
using System.Text;

namespace Motoristas.Handlers.Models
{
    public class EnderecoModel
    {
        public string Descricao { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Cep { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public CoordenadasModel Coordenadas { get; set; }
    }

    public class CoordenadasModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
