using Wappa.Motoristas.API.Application.Commands;
using Wappa.Motoristas.API.Models;

namespace Wappa.Motoristas.API.Application.Extensions
{
	public static class AdicionarMotoristaCommandExtension
    {
        public static Motorista MapearMotorista(this AdicionarMotoristaCommand message)
        {
            var endereco = new Endereco
            {
                Logradouro = message.Endereco.Logradouro,
                Numero = message.Endereco.Numero,
                Complemento = message.Endereco.Complemento,
                Bairro = message.Endereco.Bairro,
                Cep = message.Endereco.Cep,
                Cidade = message.Endereco.Cidade,
                Estado = message.Endereco.Estado
            };

            var carro = new Carro(message.Carro.Marca, message.Carro.Modelo, message.Carro.Placa);

            var motorista = new Motorista(message.Nome, message.SobreNome);

            motorista.Carro = carro;
            motorista.Endereco = endereco;

            return motorista;
        }
    }
}
