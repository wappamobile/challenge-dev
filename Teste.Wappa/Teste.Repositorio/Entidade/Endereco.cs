using System;
using System.Collections.Generic;
using System.Text;
using Teste.Repositorio.Interface;

namespace Teste.Repositorio.Entidade
{
    public class Endereco : IEntidade
    {
        public int? Id { get; set; }
        public int? IdMotorista { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public int CEP { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
