using Wappa.Core.DomainObjects;
using System;

namespace Wappa.Motoristas.API.Models
{
	public class Endereco: Entity
	{
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cep { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public Guid MotoristaId { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude{ get; set; }


        public Motorista Motorista { get; protected set; }

		public Endereco(){}

        public Endereco(string logradouro, string numero, string complemento, string bairro, string cep, string cidade, string estado)
        {
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cep = cep;
            Cidade = cidade;
            Estado = estado;
        }
    }
}
