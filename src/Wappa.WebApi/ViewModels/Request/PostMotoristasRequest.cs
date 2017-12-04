using System;
using Wappa.Domain.Common;

namespace Wappa.WebApi.ViewModels.Request
{
    public class PostMotoristasRequest
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int CarroId { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public int CidadeId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
