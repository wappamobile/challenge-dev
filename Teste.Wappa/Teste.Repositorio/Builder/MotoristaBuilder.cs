using System;
using System.Collections.Generic;
using System.Text;
using Teste.Repositorio.Entidade;

namespace Teste.Repositorio.Builder
{
    public class MotoristaBuilder
    {
        private Motorista motorista;
        
        public MotoristaBuilder()
        {
        }

        public MotoristaBuilder Novo(int? id, string nome, string sobrenome)
        {
            motorista = new Motorista
            {
                ID = id,
                Nome = nome,
                Sobrenome = sobrenome
            };

            return this;
        }

        public MotoristaBuilder ComCarro(int? id, int? idModelo, string modelo, string placa)
        {
            motorista.Carro = new Carro
            {
                Id = id,
                IdMotorista = motorista.ID,
                Modelo = new Modelo { Id = idModelo, Descricao = modelo },
                Placa = placa
            };

            return this;
        }

        public MotoristaBuilder ComCarro(int? id, int? idMarca, string marca, int? idModelo, string modelo, string placa)
        {
            motorista.Carro = new Carro
            {
                Id = id,
                IdMotorista = motorista.ID,
                Modelo = new Modelo { Id = idModelo, Descricao = modelo, Marca = new Marca { Id = idMarca, Descricao = marca} },
                Placa = placa
            };

            return this;
        }

        public MotoristaBuilder ComEndereco(int? id, string logradouro, string numero, string complemento, string bairro, string cidade, string estado, int cep, double latitude, double longitude)
        {
            motorista.Endereco = new Endereco
            {
                Id = id,
                IdMotorista = motorista.ID,
                Logradouro = logradouro,
                Numero = numero,
                Complemento = complemento,
                Bairro = bairro,
                Cidade = cidade,
                Estado = estado,
                CEP = cep,
                Latitude = latitude,
                Longitude = longitude
            };

            return this;
        }

        public Motorista Criar() {
            return motorista;
        }

    }
}
