using ApplicationCore.Entity;
using Infra.Data;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using WebApi.ViewModels.Request;

namespace Test
{
    public static class ModelHelper
    {
        public static MotoristaCadastroPostRequest ObterMotoristaPostRequest()
        {
            return new MotoristaCadastroPostRequest
            {
                CEP = "01002020",
                Cidade = "São Paulo",
                Complemento = Guid.NewGuid().ToString(),
                Logradouro = Guid.NewGuid().ToString(),
                Numero = 10,
                UF = "SP",
                MarcaCarro = Guid.NewGuid().ToString(),
                ModeloCarro = Guid.NewGuid().ToString(),
                PlacaCarro = Guid.NewGuid().ToString(),
                Nome = Guid.NewGuid().ToString(),
                Sobrenome = Guid.NewGuid().ToString()
            };
        }

        public static MotoristaCadastroPutRequest ObterMotoristaPutRequest(Context context)
        {
            var cadastro = CadastrarMotorista(context);

            return new MotoristaCadastroPutRequest
            {
                CEP = "01002020",
                Cidade = "São Paulo",
                Complemento = Guid.NewGuid().ToString(),
                Logradouro = Guid.NewGuid().ToString(),
                Numero = 10,
                UF = "SP",
                MarcaCarro = Guid.NewGuid().ToString(),
                ModeloCarro = Guid.NewGuid().ToString(),
                PlacaCarro = Guid.NewGuid().ToString(),
                Nome = Guid.NewGuid().ToString(),
                Sobrenome = Guid.NewGuid().ToString(),
                MotoristaId = cadastro.MotoristaId
            };
        }

        public static Motorista CadastrarMotorista(Context context, string nome = null, string sobrenome = null)
        {
            var carro = new Carro
            {
                Marca = Guid.NewGuid().ToString(),
                Modelo = Guid.NewGuid().ToString(),
                Placa = Guid.NewGuid().ToString(),
                Ativo = true,
                DataCadastro = DateTime.Now
            };

            context.Carros.Add(carro);
            context.SaveChanges();

            var endereco = new Endereco()
            {
                CEP = "01002020",
                Cidade = "São Paulo",
                Complemento = Guid.NewGuid().ToString(),
                Logradouro = Guid.NewGuid().ToString(),
                Numero = 10,
                UF = "SP",
                DataCadastro = DateTime.Now,
                Ativo = true
            };

            context.Enderecos.Add(endereco);
            context.SaveChanges();

            var motorista = new Motorista()
            {
                Nome = nome != null ? nome : Guid.NewGuid().ToString(),
                Sobrenome = sobrenome != null ? sobrenome : Guid.NewGuid().ToString(),
                CarroId = carro.CarroId,
                EnderecoId = endereco.EnderecoId,
                DataCadastro = DateTime.Now,
                Ativo = true
            };

            context.Motoristas.Add(motorista);
            context.SaveChanges();

            return motorista;
        }
    }
}
