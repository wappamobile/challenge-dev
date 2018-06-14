using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.API.Contratos;

namespace Teste.API.Builder
{
    public class MotoristaBuilder
    {
        private Motorista motorista;

        public MotoristaBuilder()
        {
        }

        public MotoristaBuilder Novo(int? id, string nome, string sobrenome)
        {
            motorista = new Motorista { Id = id, Nome = nome, Sobrenome = sobrenome };
            return this;
        }

        public MotoristaBuilder ComCarro(int? id, int? idMarca, string marca, int? idModelo, string modelo, string placa)
        {
            motorista.Carro = new Carro { Id = id, IdMarca = idMarca, Marca = marca, IdModelo = idModelo, Modelo = modelo, Placa = placa };
            return this;
        }

        public MotoristaBuilder ComEndereco(int? id, string logradouro, string numero, string complemento, string bairro, string cidade, string estado, int cep, double? latitude, double? longitude)
        {
            motorista.Endereco = new Endereco {Id = id, Logradouro = logradouro, Numero = numero, Complemento = complemento, Bairro = bairro, Cidade = cidade, Estado = estado, CEP = cep, Latitude = latitude, Longitude = longitude };
            return this;
        }

        private Exception ObterException()
        {
            return new Exception("O motorista precisa ser criado.");
        }

        public Motorista Criar()
        {
            return motorista;
        }

    }
}
