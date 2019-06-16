using Motoristas.Core;
using Motoristas.Core.Data;
using Motoristas.Core.Services;
using Motoristas.Handlers;
using Motoristas.Handlers.Commands;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace Motoristas.Tests
{
    public class PerfilMotoristaRequestHandlerTests
    {


        [Fact]
        public async void Can_handle_registrar_perfil_request()
        {
            var service = Substitute.For<IGeolocationService>();
            var repository = Substitute.For<IPerfilMotoristaRepository>();
            var identityGenerator = Substitute.For<IIdentityGenerator<string>>();

            var handler = new PerfilMotoristaRequestHandler(identityGenerator, repository, service);

            var request = new RegistrarPerfilMotorista(new Handlers.Models.MotoristaModel
            {
                Nome = "PrimeiroNome",
                UltimoNome = "Sobrenome",
                Veiculo = new Handlers.Models.VeiculoModel
                {
                    Marca = "MarcaCarro",
                    Modelo = "ModeloCarro",
                    Placa = "PLA-0123"
                },
                Endereco = new Handlers.Models.EnderecoModel
                {
                    Bairro = "Bairro Teste",
                    Cep = "09812-180",
                    Cidade = "Cidade Teste",
                    Complemento = "Complemento Teste",
                    Descricao = "Avenida Paulista, 1009",
                    Estado = "SP",
                    Logradouro = "Avenida Paulista, 1009",
                    Numero = "1009A"
                }
            });

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
            Assert.NotEmpty(result.PerfilId);
        }

        [Fact]
        public async void Can_handle_obter_perfil_request()
        {
            var service = Substitute.For<IGeolocationService>();
            var repository = Substitute.For<IPerfilMotoristaRepository>();
            var identityGenerator = Substitute.For<IIdentityGenerator<string>>();

            var handler = new PerfilMotoristaRequestHandler(identityGenerator, repository, service);

            var request = new ObterPerfilMotorista("1234");

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result);
        }

        [Fact]
        public async void Can_handle_remover_perfil_request()
        {
            var service = Substitute.For<IGeolocationService>();
            var repository = Substitute.For<IPerfilMotoristaRepository>();
            var identityGenerator = Substitute.For<IIdentityGenerator<string>>();

            var handler = new PerfilMotoristaRequestHandler(identityGenerator, repository, service);

            var request = new RemoverPerfilMotorista("1234");

            await Assert.ThrowsAsync<Exception>(() => handler.Handle(request, CancellationToken.None));

        }

        [Fact]
        public async void Can_handle_listar_perfil_request()
        {
            var service = Substitute.For<IGeolocationService>();
            var repository = Substitute.For<IPerfilMotoristaRepository>();
            var identityGenerator = Substitute.For<IIdentityGenerator<string>>();

            var handler = new PerfilMotoristaRequestHandler(identityGenerator, repository, service);

            var request = new PerfilMotoristaQuery();
            request.Sort = "Nome";

            var result = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(result.Lista);
            Assert.NotEmpty(result.Lista);
        }
    }
}

