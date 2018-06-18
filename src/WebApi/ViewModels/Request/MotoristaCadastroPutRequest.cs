using ApplicationCore.Entity;
using System.ComponentModel.DataAnnotations;

namespace WebApi.ViewModels.Request
{
    public class MotoristaCadastroPutRequest
    {
        [Required]
        public int MotoristaId { get; set; }
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
                MotoristaId = this.MotoristaId,
                Nome = this.Nome,
                Sobrenome = this.Sobrenome
            };
        }

        internal Carro ToCarroModel(int carroId)
        {
            return new Carro
            {
                CarroId = carroId,
                Marca = this.MarcaCarro,
                Modelo = this.ModeloCarro,
                Placa = this.PlacaCarro
            };
        }

        internal Endereco ToEnderecoModel(int enderecoId)
        {
            return new Endereco
            {
                EnderecoId = enderecoId,
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
