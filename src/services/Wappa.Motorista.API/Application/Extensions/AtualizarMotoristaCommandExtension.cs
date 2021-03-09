using Wappa.Motoristas.API.Application.Commands;
using Wappa.Motoristas.API.Models;

namespace Wappa.Motoristas.API.Application.Extensions
{
	public static class AtualizarMotoristaCommandExtension
    {
        public static Motorista MapearMotorista(this AtualizarMotoristaCommand message, Motorista motorista)
        {
            motorista.Carro.Marca = message.Carro.Marca;
            motorista.Carro.Modelo = message.Carro.Modelo;
            motorista.Carro.Placa = message.Carro.Placa;

            motorista.Endereco.Logradouro = message.Endereco.Logradouro;
            motorista.Endereco.Numero = message.Endereco.Numero;
            motorista.Endereco.Complemento = message.Endereco.Complemento;
            motorista.Endereco.Bairro = message.Endereco.Bairro;
            motorista.Endereco.Cep = message.Endereco.Cep;
            motorista.Endereco.Cidade = message.Endereco.Cidade;
            motorista.Endereco.Estado = message.Endereco.Estado;

            motorista.Nome = message.Nome;
            motorista.SobreNome = message.SobreNome;

            return motorista;
        }
    }
}
