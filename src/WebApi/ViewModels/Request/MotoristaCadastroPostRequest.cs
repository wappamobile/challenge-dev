using ApplicationCore.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.Request
{
    public class MotoristaCadastroPostRequest
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sobrenome { get; set; }

        [Required]
        public string MarcaCarro { get; set; }
        [Required]
        public string ModeloCarro { get; set; }
        [Required]
        public string PlacaCarro { get; set; }

        [Required]
        public string Logradouro { get; set; }
        [Required]
        public int Numero { get; set; }
        public string Complemento { get; set; }
        [Required]
        public string CEP { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string UF { get; set; }

        internal Motorista ToMotoristaModel()
        {
            return new Motorista
            {
                Nome = this.Nome,
                Sobrenome = this.Sobrenome
            };
        }

        internal Carro ToCarroModel()
        {
            return new Carro
            {
                Marca = this.MarcaCarro,
                Modelo = this.ModeloCarro,
                Placa = this.PlacaCarro
            };
        }

        internal Endereco ToEnderecoModel()
        {
            return new Endereco
            {
                CEP = this.CEP,
                Cidade = this.Cidade,
                Complemento = this.Complemento,
                Logradouro = this.Logradouro,
                Numero = this.Numero,
                UF = this.UF
            };
        }
    }
}
