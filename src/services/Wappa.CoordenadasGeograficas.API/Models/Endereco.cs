using System;

namespace Wappa.CoordenadasGeograficas.API.Models
{
	public class Endereco
	{
		public string Logradouro { get; set; }
		public string Numero { get; set; }
		public string Complemento { get; set; }
		public string Bairro { get; set; }
		public string Cep { get; set; }
		public string Cidade { get; set; }
		public string Estado { get; set; }

		public string Consultar()
		{
			return string.Join(Logradouro, "+", Numero, "+", Complemento, "+", Bairro, "+", Cep, "+", Cidade, "+", Estado);
		}
	}
}